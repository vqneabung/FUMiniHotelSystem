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
    /// Interaction logic for ManageCustomerPage.xaml
    /// </summary>
    public partial class ManageCustomerPage : Window
    {   

        private readonly ICustomerService _customerService;
        public ManageCustomerPage(ICustomerService customerService)
        {
            InitializeComponent();
            _customerService = customerService;
            LoadCustomer();
        }

        public void LoadCustomer()
        {
            var customers = _customerService.GetAllCustomers();
            dgCustomer.ItemsSource = null;
            dgCustomer.ItemsSource = _customerService.GetAllCustomers();
        }

        private void dgCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var customer = dgCustomer.SelectedItem as Customer;
            if (customer != null)
            {
                txtEmail.Text = customer.EmailAddress;
                txtFullName.Text = customer.CustomerFullName;
                txtTelephone.Text = customer.Telephone;
                txtBirthday.Text = customer.CustomerBirthday.ToString("d/M/yyyy");
            }
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
            if (txtFullName.Text != "" && txtTelephone.Text != "" && txtBirthday.Text != "") {
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
            } else
            {
                MessageBox.Show("Please select customer!");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReturnToAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Close();
        }
    }
}
