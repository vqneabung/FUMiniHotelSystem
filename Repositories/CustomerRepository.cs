using BussinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerDAO _customerDAO;

        public CustomerRepository(CustomerDAO customerDAO)
        {
            _customerDAO = customerDAO;
        }

        public void AddCustomer(Customer customer)
        {
            _customerDAO.AddCustomer(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            _customerDAO.DeleteCustomer(customerId);
        }

        public List<Customer> GetAllCustomers()
        {
           return _customerDAO.GetAllCustomers();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _customerDAO.GetCustomerById(customerId);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerDAO.UpdateCustomer(customer);
        }

        public Customer GetCustomerByEmail(string customerEmail)
        {
            return _customerDAO.GetCustomerByEmail(customerEmail);
        }
    }
}
