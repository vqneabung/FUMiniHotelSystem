﻿using Microsoft.Extensions.Configuration;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public readonly IConfiguration configuration;
        public readonly ICustomerService customerService;

        public Login()
        {
            InitializeComponent();
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            customerService = new CustomerService();
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            var adminEmail = configuration["AdminAccount:Email"];
            var adminPassword = configuration["AdminAccount:Password"];

            if (adminEmail == txtEmail.Text)
            {
                if (adminPassword == txtPassword.Password)
                {

                }
            }
            else if (customerService.GetCustomerByEmail(txtEmail.Text) != null)
            {
                if (customerService.GetCustomerByEmail(txtEmail.Text).Password == txtPassword.Password)
                {
                    HomePage homePage = new HomePage();
                    homePage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid email or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}