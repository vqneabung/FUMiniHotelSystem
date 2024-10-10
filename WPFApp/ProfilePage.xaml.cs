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
        public readonly ICustomerService _customerService;

        public ProfilePage(ICustomerService customerService)
        {
            InitializeComponent();
            _customerService = customerService;
            GetCustomerInfo();
        }


        public void GetCustomerInfo()
        {
            var customer = _customerService.GetCustomerByEmail(UserData.Email);
            txtEmail.Text = customer.EmailAddress;
            txtFullName.Text = customer.CustomerFullName;
            txtTelephone.Text = customer.Telephone;
            txtBirthday.Text = customer.CustomerBirthday.ToString("d/M/yyyy");
        }




        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtFullName.IsReadOnly == true)
            {
                txtFullName.IsReadOnly = false; txtTelephone.IsReadOnly = false; txtBirthday.IsReadOnly = false;
            }
            else
            {
                txtFullName.IsReadOnly = true; txtTelephone.IsReadOnly = true; txtBirthday.IsReadOnly = true;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var updateFullName = txtFullName.Text;
            var updateTelephone = txtTelephone.Text;
            var updateBirtday = txtBirthday.Text;

            var customer = _customerService.GetCustomerByEmail(txtEmail.Text);
            customer.Telephone = updateTelephone;
            customer.CustomerFullName = updateFullName;

            if (DateTime.TryParseExact(updateBirtday, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                customer.CustomerBirthday = parsedDate;
            }
            else
            {
                MessageBox.Show("Invalid date format. Please use d/M/yyyy.");
                return;
            }

            _customerService.UpdateCustomer(customer);

            MessageBox.Show("Update successfully!");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            var homePage = new HomePage();
            homePage.Show();
            this.Close();
        }
    }
}
