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
    /// Interaction logic for BookingHistoryPageForCustomer.xaml
    /// </summary>
    public partial class BookingHistoryPageForCustomer : Window
    {

        private readonly IBookingHistoryService _bookingHistoryService;
        private readonly IRoomService _roomService;
        private readonly ICustomerService _customerService;
        public BookingHistoryPageForCustomer(IBookingHistoryService bookingHistoryService, IRoomService roomService, ICustomerService customerService)
        {
            InitializeComponent();
            _bookingHistoryService = bookingHistoryService;
            _roomService = roomService;
            _customerService = customerService;
            SetupBookingHistoryDataGrid();
            LoadBookingHistory();
       
        }

        public void LoadBookingHistory()
        {
            dgBookingHistory.ItemsSource = null;
            dgBookingHistory.ItemsSource = _bookingHistoryService.GetAllBookingHistories().Where(bh => _customerService.GetCustomerById(bh.CustomerID).EmailAddress == UserData.Email);
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
                txtCustomerName.Text = _customerService.GetCustomerById(bookingHistory.CustomerID).ToString();
                txtRoomNumber.Text = _roomService.GetRoomById(bookingHistory.BookingID).ToString();
                txtCheckInDate.Text = bookingHistory.CheckInDate.ToString("d/M/yyyy");
                txtCheckOutDate.Text = bookingHistory.CheckOutDate.ToString("d/M/yyyy");
                txtBookingStatus.Text = bookingHistory.BookingStatus.ToString();
                txtNumberOfPeople.Text = bookingHistory.NumberOfPeople.ToString();
                txtTotalPrice.Text = bookingHistory.TotalPrice.ToString();
            }
        }

        private void btnReturnToHomePage_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Close();
        }
    }
    
}
