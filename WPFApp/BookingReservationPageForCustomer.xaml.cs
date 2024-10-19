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
    /// Interaction logic for BookingHistoryPageForCustomer.xaml
    /// </summary>
    public partial class BookingReservationPageForCustomer : Window
    {

        private readonly IBookingReservationService _bookingReservationService;
        //private readonly IRoomInformationService _roomInformationService;
        private readonly ICustomerService _customerService;
        private readonly IBookingDetailService _bookingDetailService;


        public BookingReservationPageForCustomer(IBookingReservationService bookingReservationService, ICustomerService customerService, IBookingDetailService bookingDetailService)
        {
            InitializeComponent();
            _bookingReservationService = bookingReservationService;
            //_roomInformationService = roomInformationService;
            _customerService = customerService;
            _bookingDetailService = bookingDetailService;
            SetupBookingReservationDataGrid();
            LoadBookingReservation();

        }

        public void LoadBookingReservation()
        {
            dgBookingReservation.ItemsSource = null;
            dgBookingReservation.ItemsSource = _bookingReservationService.GetAllBookingReservations().Where(bh => _customerService.GetCustomerById(bh.CustomerId).EmailAddress == UserData.Email);
        }   

        public void SetupBookingReservationDataGrid()
        {
            dgBookingReservation.Columns.Clear();
            
        }

        private void dgBookingReservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var bookingReservation = dgBookingReservation.SelectedItem as BookingReservation;
            if (bookingReservation != null)
            {
                txtBookingID.Text = bookingReservation.BookingReservationId.ToString();
                txtCustomerEmail.Text = bookingReservation.Customer.EmailAddress;
                txtCustomerName.Text = bookingReservation.Customer.CustomerFullName;
                txtBookingStatus.Text = bookingReservation.BookingStatus.ToString();
                txtTotalPrice.Text = bookingReservation.TotalPrice.ToString();

     
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
