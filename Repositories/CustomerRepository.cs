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
        public static CustomerDAO CustomerDAO = new CustomerDAO();

        public void AddCustomer(Customer customer)
        {
            CustomerDAO.AddCustomer(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            CustomerDAO.DeleteCustomer(customerId);
        }

        public List<Customer> GetAllCustomers()
        {
           return CustomerDAO.GetAllCustomers();
        }

        public Customer GetCustomerById(int customerId)
        {
            return CustomerDAO.GetCustomerById(customerId);
        }

        public void UpdateCustomer(Customer customer)
        {
            CustomerDAO.UpdateCustomer(customer);
        }

        public Customer GetCustomerByEmail(string customerEmail)
        {
            return CustomerDAO.GetCustomerByEmail(customerEmail);
        }
    }
}
