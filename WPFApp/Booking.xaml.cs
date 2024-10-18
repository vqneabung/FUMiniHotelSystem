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
        public readonly IRoomInformationService _roomInformationService;
        public readonly IRoomTypeService _roomTypeService;
        public readonly ICustomerService _customerService;
        public readonly IBookingReservationService _bookingReservationService;
        public readonly IBookingDetailService _bookingDetailService;


        public Booking(IRoomInformationService roomInformationService, IRoomTypeService roomTypeService, ICustomerService customerService, IBookingReservationService bookingReservationService, IBookingDetailService bookingDetailService)
        {
            InitializeComponent();
            _roomInformationService = roomInformationService;
            _roomTypeService = roomTypeService;
            _customerService = customerService;
            _bookingReservationService = bookingReservationService;
            LoadRoom();
            LoadRoomType();
            txtStartDay.Text = DateTime.Now.ToString("d/M/yyyy");
            _bookingDetailService = bookingDetailService;
        }

        public void LoadRoom()
        {
            var rooms = _roomInformationService.GetAllRoomInformations();
            if (rooms.Count != 0)
            {
                dgRoom.ItemsSource = null;
                dgRoom.ItemsSource = rooms;
                SetupRoomDataGrid();
            } else
            {
                MessageBox.Show("Not room is here");
                return;
            }
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
                if (string.IsNullOrEmpty(txtEmail.Text) ||  string.IsNullOrEmpty(txtStartDay.Text) || string.IsNullOrEmpty(txtEndDay.Text))
                {
                    MessageBox.Show("Please fill in all fields");
                    return;
                }
                var room = _roomInformationService.GetRoomInformationByRoomNumber(cboRoom.SelectedValue.ToString());
                var customer = _customerService.GetCustomerByEmail(txtEmail.Text);
                if (customer == null)
                {
                    var newCustomer = new Customer
                    {
                        EmailAddress = txtEmail.Text,
                        CustomerFullName = txtFullName.Text,
                        Telephone = txtTelephone.Text,
                        CustomerBirthday = DateOnly.Parse(txtBirthday.Text),
                        CustomerStatus = 1,
                        Password = "123456"
                    };
                    _customerService.AddCustomer(newCustomer);
                    customer = _customerService.GetCustomerByEmail(txtEmail.Text);
                }
                var booking = new BookingReservation
                {
                    //RoomID = room.RoomID,
                    //CustomerID = customer.CustomerID,
                    //BookingStatus = 1,
                    //CheckInDate = DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null),
                    //CheckOutDate = DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null),
                    //NumberOfPeople = int.Parse(txtNumberPeople.Text),
                    //TotalPrice = double.Parse(room.RoomPricePerDate.ToString()) * ((double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days < 0 ? throw new Exception() : (double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days)

                    BookingDate = DateOnly.FromDateTime(DateTime.Now),
                    CustomerId = customer.CustomerId,
                    TotalPrice = decimal.Parse(room.RoomPricePerDay.ToString()) * ((decimal)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days < 0 ? throw new Exception() : (decimal)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days),
                    BookingStatus = 1,

                };
                if (_roomInformationService.GetRoomInformationById(room.RoomId).RoomStatus == 0)
                {
                    MessageBox.Show("Room is not available");
                    return;
                } else
                {
                    _roomInformationService.GetRoomInformationById(room.RoomId).RoomStatus = 0;
                }

                _bookingReservationService.AddBookingReservation(booking);

                var bookingDetail = _bookingDetailService.GetBookingDetailByBookingReserveId(booking.BookingReservationId);

                bookingDetail.RoomId = room.RoomId;
                bookingDetail.StartDate = DateOnly.Parse(txtStartDay.Text);
                bookingDetail.EndDate = DateOnly.Parse(txtEndDay.Text);

                LoadRoom();
                
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
            try {
                cboRoom.ItemsSource = _roomInformationService.GetAllRoomInformations().Where(r => r.RoomTypeId == (int)cboRoomType.SelectedValue && r.RoomStatus == 1)
                .Select(r => r.RoomNumber);
                cboRoom.SelectedIndex = 0;
                txtStatus.Text = "Available";

            } catch (Exception ex)
            {

            }
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            RoomInformation roomSelected = dgRoom.SelectedItem as RoomInformation;
            if (roomSelected == null)
            {
                MessageBox.Show("Please select a room in data grid");
                return;
            } else if (_roomInformationService.GetRoomInformationByRoomNumber(roomSelected.RoomNumber) == null)
            {
                MessageBox.Show("Room not found");
                return;
            } else if (_roomInformationService.GetRoomInformationByRoomNumber(roomSelected.RoomNumber).RoomStatus == 1)
            {

                MessageBox.Show("This room is available");
                return;

            }
            else
            {
                roomSelected.RoomStatus = 1;
                MessageBox.Show("Checkout successfully!");
                LoadRoom();
            }


        }

        private void btnCalculatePrice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var totalPrice = double.Parse(_roomInformationService.GetRoomInformationByRoomNumber(cboRoom.SelectedValue.ToString()).RoomPricePerDay.ToString()) * ((double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days < 0 ? throw new Exception() : (double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days);
                lbTotalPrice.Content = "Price: " + totalPrice.ToString() + "Đ";

            } catch (Exception ex)
            {
                MessageBox.Show("Invalid date format. Please use d/M/yyyy.");
                return;
            }
        }

        private void cboRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            lbRoomPrice.Content = "Room Price: " + _roomInformationService.GetRoomInformationByRoomNumber(cboRoom.SelectedValue.ToString()).RoomPricePerDay.ToString();
        }
    }
}
