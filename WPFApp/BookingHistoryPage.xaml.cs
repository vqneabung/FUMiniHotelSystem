using BussinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for BookingHistoryPage.xaml
    /// </summary>
    public partial class BookingHistoryPage : Window
    {

        private readonly IBookingReservationService _bookingHistoryService;
        private readonly IRoomInformationService _roomService;
        private readonly ICustomerService _customerService;
        public BookingHistoryPage(IBookingReservationService bookingHistoryService, IRoomInformationService roomService, ICustomerService customerService)
        {
            InitializeComponent();
            _bookingHistoryService = bookingHistoryService;
            _roomService = roomService;
            _customerService = customerService;
            LoadBookingHistory();
            SetupBookingHistoryDataGrid();
        }

        public void LoadBookingHistory()
        {
            var bookingHistories = _bookingHistoryService.GetAllBookingHistories();
            dgBookingHistory.ItemsSource = null;
            dgBookingHistory.ItemsSource = _bookingHistoryService.GetAllBookingHistories();
        }

        public void SetupBookingHistoryDataGrid()
        {
            dgBookingHistory.AutoGenerateColumns = false;
            dgBookingHistory.Columns.Clear();

            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Booking ID", Binding = new Binding("BookingID") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Customer ID", Binding = new Binding("CustomerID") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Customer Name", Binding = new Binding("Customer.CustomerFullName") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Room Number", Binding = new Binding("Room.RoomNumber") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Check In Date", Binding = new Binding("CheckInDate") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Check Out Date", Binding = new Binding("CheckOutDate") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "People Of Number", Binding = new Binding("NumberOfPeople") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Booking Status", Binding = new Binding("BookingStatus") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Total Price", Binding = new Binding("TotalPrice") });
        }

        private void dgBookingHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var bookingHistory = dgBookingHistory.SelectedItem as BookingHistory;
            if (bookingHistory != null)
            {
                txtBookingID.Text = bookingHistory.BookingID.ToString();
                txtCustomerEmail.Text = _customerService.GetCustomerById(bookingHistory.CustomerID).EmailAddress.ToString();
                txtRoomNumber.Text = _roomService.GetRoomById(bookingHistory.RoomID).RoomNumber.ToString();
                txtNumberOfPeople.Text = bookingHistory.NumberOfPeople.ToString();
                txtCheckInDate.Text = bookingHistory.CheckInDate.ToString("d/M/yyyy");
                txtCheckOutDate.Text = bookingHistory.CheckOutDate.ToString("d/M/yyyy");
                txtBookingStatus.Text = bookingHistory.BookingStatus.ToString();
                txtTotalPrice.Text = bookingHistory.TotalPrice.ToString();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var updateBookingID = txtBookingID.Text;
                var updateRoomNumber = txtRoomNumber.Text;
                var updateCheckInDate = DateTime.ParseExact(txtCheckInDate.Text, "d/M/yyyy", null);
                var updateCheckOutDate = DateTime.ParseExact(txtCheckOutDate.Text, "d/M/yyyy", null);
                var updateNumberOfPeople = txtNumberOfPeople.Text;
                var updateBookingStatus = txtBookingStatus.Text;
                var updateTotalPrice = txtTotalPrice.Text;

                var bookingHistory = _bookingHistoryService.GetBookingHistoryById(Convert.ToInt32(updateBookingID));

                bookingHistory.RoomID = _roomService.GetRoomById(Convert.ToInt32(updateRoomNumber)).RoomID;
                bookingHistory.CheckInDate = updateCheckInDate;
                bookingHistory.CheckOutDate = updateCheckOutDate;
                bookingHistory.NumberOfPeople = int.Parse(updateNumberOfPeople);
                bookingHistory.BookingStatus = Convert.ToInt32(updateBookingStatus);
                bookingHistory.TotalPrice = Convert.ToDouble(updateTotalPrice);

                _bookingHistoryService.UpdateBookingHistory(bookingHistory);
                MessageBox.Show("Update successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please fill all the fields");
            }


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var bookingID = int.Parse(txtBookingID.Text);
            if (_bookingHistoryService.GetBookingHistoryById(bookingID) != null) { 
                _bookingHistoryService.DeleteBookingHistory(bookingID);
                MessageBox.Show("Delete successfully!");
            }
            else
            {
                MessageBox.Show("Please select booking history!");
            }
        }

        private void btnReturnToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();   

        }
    }
}
