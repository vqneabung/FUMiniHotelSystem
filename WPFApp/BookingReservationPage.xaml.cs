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
            LoadBookingReservation();
       
        }

        public void LoadBookingReservation()
        {
            var bookingDetail = _bookingDetailService.GetAllBookingDetails();
            dgBookingDetails.ItemsSource = null;
            dgBookingDetails.ItemsSource = bookingDetail;
        }

        private void dgBookingDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var bookingReservation = dgBookingHistory.SelectedItem as BookingReservation;
            //if (bookingReservation != null)
            //{
            //txtBookingID.Text = bookingReservation.BookingReservationId.ToString();
            //txtCustomerEmail.Text = customer.EmailAddress.ToString();
            //txtCustomerName.Text = customer.CustomerFullName.ToString();
            //txtRoomNumber.Text = bookingDetail.Room.RoomNumber.ToString();
            //txtCheckInDate.Text = bookingDetail.StartDate.ToString();
            //txtCheckOutDate.Text = bookingDetail.EndDate.ToString();
            //txtBookingStatus.Text = bookingDetail.Room.RoomStatus.ToString();
            //txtTotalPrice.Text = bookingDetail.ActualPrice.ToString();
            //    var customer = _customerService.GetCustomerById(bookingReservation.CustomerId);
            //    if (customer != null)
            //    {
            //        txtCustomerEmail.Text = customer.EmailAddress.ToString();
            //        txtCustomerName.Text = customer.CustomerFullName.ToString();
            //    }
            //    var bookingDetail = _bookingDetailService.GetAllBookingDetails().FirstOrDefault(bd => bd.BookingReservationId == bookingReservation.BookingReservationId);
            //    if (bookingDetail != null)
            //    {

            //    }
            //}

            var bookingDetail = dgBookingDetails.SelectedItem as BookingDetail;

            if (bookingDetail != null)
            {
                txtBookingID.Text = bookingDetail.BookingReservationId.ToString();
                txtCustomerEmail.Text = bookingDetail.BookingReservation.Customer.EmailAddress.ToString();
                txtCustomerName.Text = bookingDetail.BookingReservation.Customer.CustomerFullName?.ToString();
                txtRoomNumber.Text = bookingDetail.Room.RoomNumber.ToString();
                txtCheckInDate.Text = bookingDetail.StartDate.ToString();
                txtCheckOutDate.Text = bookingDetail.EndDate.ToString();
                txtBookingStatus.Text = bookingDetail.Room.RoomStatus.ToString();
                txtTotalPrice.Text = bookingDetail.ActualPrice.ToString();
            }



        }

        //private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {

        //        var bookingDetail = _bookingDetailService.GetBookingDetailByBookingReservationAndRoomInformation(Convert.ToInt32(txtBookingID.Text), Convert.ToInt32(txtRoomNumber.Text));

        //        var updateBookingReservationID = txtBookingID.Text;
        //        var updateRoomNumber = txtRoomNumber.Text;
        //        var updateCheckInDate = DateOnly.ParseExact(txtCheckInDate.Text, "d/M/yyyy", null);
        //        var updateCheckOutDate = DateOnly.ParseExact(txtCheckOutDate.Text, "d/M/yyyy", null);
        //        var updateBookingStatus = txtBookingStatus.Text;
        //        var updateTotalPrice = txtTotalPrice.Text;


        //        bookingDetail.RoomId = _roomInformationService.GetRoomInformationByRoomNumber(updateRoomNumber).RoomId;
        //        bookingDetail.StartDate = updateCheckInDate;
        //        bookingDetail.EndDate = updateCheckOutDate;
        //        bookingDetail.BookingReservation.BookingStatus = Convert.ToByte(updateBookingStatus);
        //        bookingDetail.BookingReservation.TotalPrice = Convert.ToDecimal(updateTotalPrice);

        //        _bookingDetailService.UpdateBookingDetail(bookingDetail);
        //        MessageBox.Show("Update successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Please fill all the fields");
        //    }


        //}

        //private void btnDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    var bookingID = int.Parse(txtBookingID.Text);
        //    var roomNumber = int.Parse(txtRoomNumber.Text);
        //    if (_bookingDetailService.GetBookingDetailByBookingReservationAndRoomInformation(bookingID, roomNumber) != null) {
        //        _bookingDetailService.DeleteBookingDetail(bookingID, roomNumber);
        //        MessageBox.Show("Delete successfully!");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select booking history!");
        //    }
        //}

        private void btnReturnToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();

        }
    }
}
