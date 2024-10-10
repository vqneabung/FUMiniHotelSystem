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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            BookingHistory bookingHistory = new BookingHistory();
            bookingHistory.Show();
            this.Close();
            
        }

        private void btnManageRoom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnManageRoomType_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBookingReservationHistory_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
