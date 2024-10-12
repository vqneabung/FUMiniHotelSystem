using BussinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : Window
    {
        public readonly IRoomService _roomService;
        public readonly IRoomTypeService _roomTypeService;
        public readonly ICustomerService _customerService;
        public readonly IBookingHistoryService _bookingService;


        public Booking(IRoomService roomService, IRoomTypeService roomTypeService, ICustomerService customerService, IBookingHistoryService bookingHistoryService)
        {
            InitializeComponent();
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _customerService = customerService;
            _bookingService = bookingHistoryService;
            LoadRoom();
            txtStartDay.Text = DateTime.Now.ToString("d/M/yyyy");
        }

        public void LoadRoom()
        {
            var rooms = _roomService.GetAllRooms();
            dgRoom.ItemsSource = null;
            SetupRoomDataGrid();
            LoadRoomType();
        }

        public void SetupRoomDataGrid()
        {
            dgRoom.AutoGenerateColumns = false; // Tắt tự động tạo cột

            // Xóa tất cả các cột trước khi thêm mới
            dgRoom.Columns.Clear();

            // Thêm các cột theo thứ tự mong muốn
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Number", Binding = new Binding("RoomNumber") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Type Name", Binding = new Binding("RoomType.RoomTypeName") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Max Capacity", Binding = new Binding("RoomMaxCapacity") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Status", Binding = new Binding("RoomStatus") });
            dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Price Per Date", Binding = new Binding("RoomPricePerDate") });

            // Đặt nguồn dữ liệu cho DataGrid
            dgRoom.ItemsSource = _roomService.GetAllRoomsIncludeRoomType();
        }

        public void LoadRoomType()
        {
            var roomTypes = _roomTypeService.GetAllRoomTypes();
            cboRoomType.ItemsSource = null;
            cboRoomType.ItemsSource = roomTypes;
            cboRoomType.DisplayMemberPath = "RoomTypeName";
            cboRoomType.SelectedValuePath = "RoomTypeID";
            cboRoomType.SelectedIndex = 0;
        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtNumberPeople.Text) || string.IsNullOrEmpty(txtStartDay.Text) || string.IsNullOrEmpty(txtEndDay.Text))
                {
                    MessageBox.Show("Please fill in all fields");
                    return;
                }
                var room = _roomService.GetRoomByRoomNumber(cboRoom.SelectedValue.ToString());
                var customer = _customerService.GetCustomerByEmail(txtEmail.Text);
                var booking = new BookingHistory
                {
                    RoomID = room.RoomID,
                    CustomerID = customer.CustomerID,
                    BookingStatus = 1,
                    CheckInDate = DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null),
                    CheckOutDate = DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null),
                    NumberOfPeople = int.Parse(txtNumberPeople.Text),
                    TotalPrice = double.Parse(room.RoomPricePerDate.ToString()) * ((double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days < 0 ? throw new Exception() : (double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days)
                };
                if (_roomService.GetRoomById(room.RoomID).RoomStatus == 0)
                {
                    MessageBox.Show("Room is not available");
                    return;
                } else
                {
                    _roomService.GetRoomById(room.RoomID).RoomStatus = 0;
                }

                _bookingService.AddBookingHistory(booking);
                
                MessageBox.Show("Booking successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Booking failed: " + ex.Message);
            }
        }

        private void btnFindCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customer = _customerService.GetCustomerByEmail(txtEmail.Text);

            if (customer != null && customer.CustomerStatus != 2)
            {
                txtFullName.Text = customer.CustomerFullName;
                txtTelephone.Text = customer.Telephone;
                txtBirthday.Text = customer.CustomerBirthday.ToString();
            }
            else
            {
                MessageBox.Show("Customer not found");

            }
        }

        private void btnReturnToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();
        }

        private void cboRoomtype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboRoom.ItemsSource = _roomService.GetAllRooms().Where(r => r.RoomTypeID == (int)cboRoomType.SelectedValue && r.RoomStatus == 1)
                .Select(r => r.RoomNumber);
            cboRoom.SelectedIndex = 0;
            txtStatus.Text = "Available";
        }

        private void txtNumberPeople_TextChanged(object sender, TextChangedEventArgs e)
        {
            var max = _roomService.GetRoomByRoomNumber(cboRoom.SelectedValue.ToString()).RoomMaxCapacity;
            if (int.TryParse(txtNumberPeople.Text, out int numberPeople))
            {
                if (numberPeople > max)
                {
                    MessageBox.Show("Please enter a number less than or equal to " + max);
                    txtNumberPeople.Text = max.ToString();
                    return;
                }
            }

            lbRoomPrice.Content = "Room Price: " + _roomService.GetRoomByRoomNumber(cboRoom.SelectedValue.ToString()).RoomPricePerDate.ToString();

        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCalculatePrice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var totalPrice = double.Parse(_roomService.GetRoomByRoomNumber(cboRoom.SelectedValue.ToString()).RoomPricePerDate.ToString()) * ((double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days < 0 ? throw new Exception() : (double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days);
                lbTotalPrice.Content = "Price: " + totalPrice.ToString() + "Đ";

            } catch (Exception ex)
            {
                MessageBox.Show("Invalid date format. Please use d/M/yyyy.");
                return;
            }
        }
    }
}
