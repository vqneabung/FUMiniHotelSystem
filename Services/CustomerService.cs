using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService()
        {
            customerRepository = new CustomerRepository();
        }

        public void AddCustomer(Customer customer)
        {
            customerRepository.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerRepository.UpdateCustomer(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            customerRepository.DeleteCustomer(customerId);
        }

        public Customer GetCustomerById(int customerId)
        {
            return customerRepository.GetCustomerById(customerId);
        }

        public ObservableCollection<Customer> GetAllCustomers()
        {
            return customerRepository.GetAllCustomers();
        }

        public Customer GetCustomerByEmail(string customerEmail)
        {
            return customerRepository.GetCustomerByEmail(customerEmail);
        }

    }
}
