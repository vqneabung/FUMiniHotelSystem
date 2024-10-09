using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Window
    {

        public ProfilePage()
        {
            InitializeComponent();
            GetCustomerInfo();
        }


        public void GetCustomerInfo()
        {
            ICustomerService customerService = new CustomerService();
            var customer = customerService.GetCustomerByEmail(UserData.Email);
            txtEmail.Text = customer.EmailAddress;
            txtFullName.Text = customer.CustomerFullName;
            txtTelephone.Text = customer.Telephone;
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.IsReadOnly == true && txtFullName.IsReadOnly == true && txtTelephone.IsReadOnly == true)
            {
                txtEmail.IsReadOnly = false; txtFullName.IsReadOnly = false; txtTelephone.IsReadOnly = false;
            }
            else
            {
                txtEmail.IsReadOnly = true; txtFullName.IsReadOnly = true; txtTelephone.IsReadOnly = true;
            }
        }
    }
}
