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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {   
 

        public HomePage()   
        {
            InitializeComponent();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var serviceProvider = App.ServiceProvider;
            ProfilePage profilePage = serviceProvider.GetRequiredService<ProfilePage>();
            profilePage.Show();
        }

        private void btnBookingHistory_Click(object sender, RoutedEventArgs e)
        {
            BookingReservationPageForCustomer bookingReservationPageForCustomer = App.ServiceProvider.GetRequiredService<BookingReservationPageForCustomer>();
            bookingReservationPageForCustomer.Show();
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
