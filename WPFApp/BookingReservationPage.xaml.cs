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
    public partial class BookingReservationPage : Window
    {

        private readonly IBookingReservationService _bookingReservationService;
        private readonly IRoomInformationService _roomInformationService;
        private readonly ICustomerService _customerService;
        private readonly IBookingDetailService _bookingDetailService;
        public BookingReservationPage(IBookingReservationService bookingReservationService, IRoomInformationService roomInformationService, ICustomerService customerService, IBookingDetailService bookingDetailService)
        {
            InitializeComponent();
            _bookingReservationService = bookingReservationService;
            _roomInformationService = roomInformationService;
            _customerService = customerService;
            _bookingDetailService = bookingDetailService;
            LoadBookingHistory();
            SetupBookingHistoryDataGrid();
       
        }

        public void LoadBookingHistory()
        {
            var bookingHistories = _bookingReservationService.GetAllBookingReservations();
            dgBookingHistory.ItemsSource = null;
            dgBookingHistory.ItemsSource = bookingHistories;
        }

        public void SetupBookingHistoryDataGrid()
        {
            dgBookingHistory.AutoGenerateColumns = false;
            dgBookingHistory.Columns.Clear();

            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Booking ID", Binding = new Binding("BookingReservationId") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Customer ID", Binding = new Binding("CustomerId") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Customer Name", Binding = new Binding("Customer.CustomerFullName") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Room Number", Binding = new Binding("BookingDetail.RoomInformation.RoomNumber") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Check In Date", Binding = new Binding("BookingDetail.StartDate") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Check Out Date", Binding = new Binding("BookingDetail.EndDate") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Booking Status", Binding = new Binding("BookingDetail.RoomInformation.RoomStatus") });
            dgBookingHistory.Columns.Add(new DataGridTextColumn { Header = "Total Price", Binding = new Binding("BookingDetail.ActualPrice") });
        }

        private void dgBookingHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bookingReservation = dgBookingHistory.SelectedItem as BookingReservation;
            if (bookingReservation != null)
            {
                txtBookingID.Text = bookingReservation.BookingReservationId.ToString();
                var customer = _customerService.GetCustomerById(bookingReservation.CustomerId);
                if (customer != null)
                {
                    txtCustomerEmail.Text = customer.EmailAddress.ToString();
                    txtCustomerName.Text = customer.CustomerFullName.ToString();
                }
                var bookingDetail = _bookingDetailService.GetAllBookingDetails().FirstOrDefault(bd => bd.BookingReservationId == bookingReservation.BookingReservationId);
                if (bookingDetail != null)
                {
                    txtRoomNumber.Text = bookingDetail.Room.RoomNumber.ToString();
                    txtCheckInDate.Text = bookingDetail.StartDate.ToString();
                    txtCheckOutDate.Text = bookingDetail.EndDate.ToString();
                    txtBookingStatus.Text = bookingDetail.Room.RoomStatus.ToString();
                    txtTotalPrice.Text = bookingDetail.ActualPrice.ToString();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var updateBookingReservationID = txtBookingID.Text;
                var updateRoomNumber = txtRoomNumber.Text;
                var updateCheckInDate = DateOnly.Parse(txtCheckInDate.Text);
                var updateCheckOutDate = DateOnly.Parse(txtCheckOutDate.Text);
                var updateBookingStatus = txtBookingStatus.Text;
                var updateTotalPrice = txtTotalPrice.Text;

                var bookingReservation = _bookingReservationService.GetBookingReservationById(Convert.ToInt32(updateBookingReservationID));
                var bookingDetail = _bookingDetailService.GetBookingDetailByBookingReserveId(Convert.ToInt32(updateBookingReservationID));


                bookingDetail.RoomId = _roomInformationService.GetRoomInformationById(Convert.ToInt32(updateRoomNumber)).RoomId;
                bookingDetail.StartDate = updateCheckInDate;
                bookingDetail.EndDate = updateCheckOutDate;
                bookingDetail.BookingReservation.BookingStatus = Convert.ToByte(updateBookingStatus);
                bookingDetail.BookingReservation.TotalPrice = Convert.ToDecimal(updateTotalPrice);

                _bookingDetailService.UpdateBookingDetail(bookingDetail);
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
            if (_bookingDetailService.GetBookingDetailByBookingReserveId(bookingID) != null) {
                _bookingDetailService.DeleteBookingDetail(bookingID);
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
