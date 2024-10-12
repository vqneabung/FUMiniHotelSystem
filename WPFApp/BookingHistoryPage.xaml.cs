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
    /// Interaction logic for BookingHistoryPage.xaml
    /// </summary>
    public partial class BookingHistoryPage : Window
    {

        private readonly IBookingHistoryService _bookingHistoryService;
        private readonly IRoomService _roomService;
        private readonly ICustomerService _customerService;
        public BookingHistoryPage(IBookingHistoryService bookingHistoryService, IRoomService roomService, ICustomerService customerService)
        {
            InitializeComponent();
            _bookingHistoryService = bookingHistoryService;
            _roomService = roomService;
            _customerService = customerService;
        }

        public void LoadBookingHistory()
        {
            var bookingHistories = _bookingHistoryService.GetAllBookingHistories();
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var updateBookingID = txtBookingID.Text;
                var updateCustomerEmail = txtCustomerEmail.Text;
                var updateRoomNumber = txtRoomNumber.Text;
                var updateCheckInDate = DateTime.ParseExact(txtCheckInDate.Text, "d/M/yyyy", null);
                var updateCheckOutDate = DateTime.ParseExact(txtCheckOutDate.Text, "d/M/yyyy", null);
                var updateBookingStatus = txtBookingStatus.Text;
                var updateTotalPrice = txtTotalPrice.Text;

                var bookingHistory = _bookingHistoryService.GetBookingHistoryById(Convert.ToInt32(updateBookingID));

                bookingHistory.CustomerID = _customerService.GetCustomerByEmail(updateCustomerEmail).CustomerID; 
                bookingHistory.RoomID = _roomService.GetRoomById(Convert.ToInt32(updateRoomNumber)).RoomID;
                bookingHistory.CheckInDate = updateCheckInDate;
                bookingHistory.CheckOutDate = updateCheckOutDate;
                bookingHistory.BookingStatus = Convert.ToInt32(updateBookingStatus);
                bookingHistory.TotalPrice = Convert.ToDouble(updateTotalPrice);

                _bookingHistoryService.(bookingHistory);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Please fill all the fields");
            }


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReturnToAdmin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
