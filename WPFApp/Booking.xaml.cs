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
            //LoadRoom();
            LoadRoomType();
            txtStartDay.Text = DateTime.Now.ToString("d/M/yyyy");
            _bookingDetailService = bookingDetailService;
        }

        //public void LoadRoom()
        //{
        //    var rooms = _roomInformationService.GetAllRoomInformations();
        //    if (rooms.Count != 0)
        //    {
        //        dgRoom.ItemsSource = null;
        //        dgRoom.ItemsSource = rooms;
        //        SetupRoomDataGrid();
        //    } else
        //    {
        //        MessageBox.Show("Not room is here");
        //        return;
        //    }
        //}

        //public void SetupRoomDataGrid()
        //{
        //    dgRoom.AutoGenerateColumns = false; // Tắt tự động tạo cột

        //    // Xóa tất cả các cột trước khi thêm mới
        //    dgRoom.Columns.Clear();

        //    // Thêm các cột theo thứ tự mong muốn
        //    dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Number", Binding = new Binding("RoomNumber") });
        //    dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Type Name", Binding = new Binding("RoomType.RoomTypeName") });
        //    dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Max Capacity", Binding = new Binding("RoomMaxCapacity") });
        //    dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Status", Binding = new Binding("RoomStatus") });
        //    dgRoom.Columns.Add(new DataGridTextColumn { Header = "Room Price Per Date", Binding = new Binding("RoomPricePerDate") });

        //}

        public void LoadRoomType()
        {
            var roomTypes = _roomTypeService.GetAllRoomTypes();
            cboRoomType.ItemsSource = null;
            cboRoomType.ItemsSource = roomTypes;
            cboRoomType.DisplayMemberPath = "RoomTypeName";
            cboRoomType.SelectedValuePath = "RoomTypeId";
            cboRoomType.SelectedIndex = 0;
        }


        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtStartDay.Text) || string.IsNullOrEmpty(txtEndDay.Text))
                {
                    MessageBox.Show("Please fill in all fields");
                    return;
                }
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
                    BookingStatus = 1,
                    TotalPrice = 0


                };

                for (int i = 0; i < dgBookingDetails.Items.Count; i++)
                {
                    var getBookingDetail = dgBookingDetails.Items[i] as BookingDetail;
                    if (_roomInformationService.GetRoomInformationById(getBookingDetail.RoomId).RoomStatus == 0)
                    {
                        MessageBox.Show("Room is not available");
                        return;
                    }
                    getBookingDetail.Room.RoomStatus = 0;
                    booking.TotalPrice += getBookingDetail.ActualPrice;
                    booking.BookingDetails.Add(getBookingDetail);
                }

                _bookingReservationService.AddBookingReservation(booking);

                //LoadRoom();

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
            try
            {
                var roomType = cboRoomType.SelectedValue;
                var rooms = _roomInformationService.GetAllRoomInformations()
                                .Where(r => r.RoomTypeId == (int)roomType)
                                .Select(r => r.RoomNumber)
                                .ToList();

                if (rooms.Any())
                {
                    cboRoom.ItemsSource = null;
                    cboRoom.ItemsSource = rooms;
                    cboRoom.SelectedIndex = 0;
                }
                else
                {
                    cboRoom.ItemsSource = null;
                }

                // Validate if cboRoom.SelectedItem is not null before proceeding
                if (cboRoom.SelectedItem != null)
                {
                    var selectedRoomInfo = _roomInformationService.GetRoomInformationByRoomNumber(cboRoom.SelectedItem.ToString());
                    if (selectedRoomInfo != null && selectedRoomInfo.RoomStatus == 1)
                    {
                        txtStatus.Text = "Available";
                    }
                    else
                    {
                        txtStatus.Text = "Not Available";
                    }
                }
                else
                {
                    txtStatus.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                cboRoom.ItemsSource = null;
            }
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            var roomInformationId = cboRoom.SelectedItem.ToString();
            var roomInformation = _roomInformationService.GetRoomInformationByRoomNumber(roomInformationId);

            if (roomInformation.RoomStatus == 0)
            {
                roomInformation.RoomStatus = 1;
                _roomInformationService.UpdateRoomInformation(roomInformation);
                MessageBox.Show("Check Out!");
                txtStatus.Text = "Available";
            }
            else
            {
                MessageBox.Show("This room is available");
            }


        }

        private void btnCalculatePrice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var totalPrice = double.Parse(_roomInformationService.GetRoomInformationByRoomNumber(cboRoom.SelectedValue.ToString()).RoomPricePerDay.ToString()) * ((double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days < 0 ? throw new Exception() : (double)(DateTime.ParseExact(txtEndDay.Text, "d/M/yyyy", null) - DateTime.ParseExact(txtStartDay.Text, "d/M/yyyy", null)).Days);
                lbTotalPrice.Content = "Price: " + totalPrice.ToString() + "Đ";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid date format. Please use d/M/yyyy.");
                return;
            }
        }

        private void cboRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Check if an item is selected
                if (cboRoom.SelectedItem != null)
                {
                    // Retrieve the selected room number and fetch room information
                    var selectedRoomNumber = cboRoom.SelectedValue?.ToString();
                    if (selectedRoomNumber != null)
                    {
                        var roomInformation = _roomInformationService.GetRoomInformationByRoomNumber(selectedRoomNumber);

                        // Check if room information is not null
                        if (roomInformation != null)
                        {
                            // Set the room status and price information
                            txtStatus.Text = roomInformation.RoomStatus == 1 ? "Available" : "Not Available";
                            lbRoomPrice.Content = "Room Price: " + roomInformation.RoomPricePerDay.ToString();
                        }
                        else
                        {   
                            MessageBox.Show("Room information not found.");
                        }
                    }
                    else
                    {
                        txtStatus.Text = "Room not selected.";
                    }
                }
                else
                {
                    // Handle the case where no room is selected
                    txtStatus.Text = "Please select a room.";
                    lbRoomPrice.Content = "Room Price: N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse the date strings with the correct format
                var startDate = DateOnly.ParseExact(txtStartDay.Text, "d/M/yyyy", null);
                var endDate = DateOnly.ParseExact(txtEndDay.Text, "d/M/yyyy", null);

                var bookingDetail = new BookingDetail
                {
                    BookingReservationId = int.Parse(cboRoom.SelectedValue.ToString()),
                    RoomId = _roomInformationService.GetRoomInformationByRoomNumber(cboRoom.SelectedValue.ToString()).RoomId,
                    Room = _roomInformationService.GetRoomInformationByRoomNumber(cboRoom.SelectedValue.ToString()),
                    StartDate = startDate,
                    EndDate = endDate,
                    ActualPrice = decimal.Parse(_roomInformationService.GetRoomInformationByRoomNumber(cboRoom.SelectedValue.ToString()).RoomPricePerDay.ToString()) * ((decimal)(endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days < 0 ? throw new Exception("End date must be after start date.") : (decimal)(endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days),

                };

                

                try
                {
                    for (int i = 0; i < dgBookingDetails.Items.Count; i++)
                    {

                        var getBookingDetailItems = dgBookingDetails.Items.GetItemAt(i) as BookingDetail;
                        var bookNumber = _roomInformationService.GetRoomInformationByRoomNumber(cboRoom.SelectedValue.ToString());
                        if (getBookingDetailItems.RoomId ==  bookNumber.RoomId)
                        {
                            MessageBox.Show("This room has selected");
                            return;
                            
                        }
                    }
                } catch (Exception ex) { }
                dgBookingDetails.Items.Add(bookingDetail);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid date format. Please use d/M/yyyy.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
