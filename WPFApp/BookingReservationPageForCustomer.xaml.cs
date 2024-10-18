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
    public partial class BookingReservationPageForCustomer : Window
    {

        private readonly IBookingReservationService _bookingReservationService;
        private readonly IRoomInformationService _roomInformationService;
        private readonly ICustomerService _customerService;
        private readonly IBookingDetailService _bookingDetailService;


        public BookingReservationPageForCustomer(IBookingReservationService bookingReservationService, IRoomInformationService roomInformationService, ICustomerService customerService, IBookingDetailService bookingDetailService)
        {
            InitializeComponent();
            _bookingReservationService = bookingReservationService;
            _roomInformationService = roomInformationService;
            _customerService = customerService;
            SetupBookingHistoryDataGrid();
            LoadBookingHistory();
            _bookingDetailService = bookingDetailService;
        }

        public void LoadBookingHistory()
        {
            dgBookingHistory.ItemsSource = null;
            dgBookingHistory.ItemsSource = _bookingReservationService.GetAllBookingReservations().Where(bh => _customerService.GetCustomerById(bh.CustomerId).EmailAddress == UserData.Email);
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

        private void btnReturnToHomePage_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            this.Close();
        }
    }
    
}
