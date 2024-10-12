using Microsoft.Extensions.DependencyInjection;
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
            ManageCustomerPage manageCustomerPage = App.ServiceProvider.GetRequiredService<ManageCustomerPage>();
            manageCustomerPage.Show();
            this.Close();
            
        }

        private void btnManageRoom_Click(object sender, RoutedEventArgs e)
        {
            ManageRoomPage manageRoomPage = App.ServiceProvider.GetRequiredService<ManageRoomPage>();
            manageRoomPage.Show();
            this.Close();
        }

        private void btnManageRoomType_Click(object sender, RoutedEventArgs e)
        {
            ManageRoomTypePage manageRoomTypePage = App.ServiceProvider.GetRequiredService<ManageRoomTypePage>();
            manageRoomTypePage.Show();
            this.Close();

        }

        private void btnBookingReservation(object sender, RoutedEventArgs e)
        {
            Booking booking = App.ServiceProvider.GetRequiredService<Booking>();
            booking.Show();
            this.Close();
        }

        private void btnLogout(object sender, RoutedEventArgs e)
        {
            LoginPage login = App.ServiceProvider.GetRequiredService<LoginPage>();
            login.Show();
            this.Close();
        }
    }
}
