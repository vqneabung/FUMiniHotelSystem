using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CustomerDAO
    {

        private readonly FuminiHotelManagementContext _context;

        public CustomerDAO(FuminiHotelManagementContext context)
        {
            _context = context;
          
        }

        public void AddCustomer(Customer customer)
        {
           customer.CustomerId = _context.Customers.Max(c => c.CustomerId) + 1;
            _context.Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = GetCustomerById(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
        }

        public Customer? GetCustomerById(int customerId)
        {
            return _context.Customers.Include(c => c.BookingReservations).FirstOrDefault(c => c.CustomerId == customerId);
        }

        public Customer? GetCustomerByEmail(string customerEmail)
        {
            return _context.Customers.Include(c => c.BookingReservations).FirstOrDefault(c => c.EmailAddress == customerEmail);
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers
                .Include(c => c.BookingReservations)
                .ToList();

        }
    }
}
