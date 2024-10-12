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
        }

        public void LoadBookingHistory()
        {
            var bookingHistories = _bookingHistoryService.GetAllBookingHistories().Where(bh => _customerService.GetCustomerById(bh.CustomerID).EmailAddress == UserData.Email);
            dgBookingHistory.ItemsSource = null;
            dgBookingHistory.ItemsSource = _bookingHistoryService.GetAllBookingHistories();
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
