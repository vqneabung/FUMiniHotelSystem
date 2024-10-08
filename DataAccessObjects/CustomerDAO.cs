using BussinessObjects;
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

        public ObservableCollection<Customer> listCustomer = new ObservableCollection<Customer>();

        public CustomerDAO()
        {

            // Thêm khách hàng đầu tiên
            Customer customer1 = new Customer
            {
                CustomerID = 1,
                CustomerFullName = "Nguyen Van A",
                CustomerBirthday = new DateTime(1999, 1, 1),
                CustomerStatus = 1,
                EmailAddress = "nguyenvana@gmail.com",
                Password = "123456",
                Telephone = "0123456789"
            };

            // Thêm khách hàng khác
            Customer customer2 = new Customer
            {
                CustomerID = 2,
                CustomerFullName = "Tran Thi B",
                CustomerBirthday = new DateTime(1998, 2, 2),
                CustomerStatus = 1,
                EmailAddress = "tranthib@gmail.com",
                Password = "abcdef",
                Telephone = "0987654321"
            };

            Customer customer3 = new Customer
            {
                CustomerID = 3,
                CustomerFullName = "Le Van C",
                CustomerBirthday = new DateTime(2000, 3, 3),
                CustomerStatus = 1,
                EmailAddress = "levanc@gmail.com",
                Password = "password123",
                Telephone = "0934567890"
            };

            Customer customer4 = new Customer
            {
                CustomerID = 4,
                CustomerFullName = "Pham Thi D",
                CustomerBirthday = new DateTime(1997, 4, 4),
                CustomerStatus = 1,
                EmailAddress = "phamthid@gmail.com",
                Password = "123abc",
                Telephone = "0912345678"
            };

            // Thêm các khách hàng vào danh sách
            listCustomer.Add(customer1);
            listCustomer.Add(customer2);
            listCustomer.Add(customer3);
            listCustomer.Add(customer4);
        }

        public void AddCustomer(Customer customer)
        {
            if (listCustomer.Count == 0)
            {
                customer.CustomerID = 1;
            }
            else
            {
                customer.CustomerID = listCustomer[listCustomer.Count - 1].CustomerID + 1;
            }


            listCustomer.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            foreach (Customer c in listCustomer)
            {
                if (c.CustomerID == customer.CustomerID)
                {
                    c.CustomerFullName = customer.CustomerFullName;
                    c.CustomerBirthday = customer.CustomerBirthday;
                    c.CustomerStatus = customer.CustomerStatus;
                    c.EmailAddress = customer.EmailAddress;
                    c.Password = customer.Password;
                    c.Telephone = customer.Telephone;
                }
            }
        }

        public void DeleteCustomer(int customerId)
        {
            foreach (Customer c in listCustomer)
            {
                if (c.CustomerID == customerId)
                {
                    listCustomer.Remove(c);
                    break;
                }
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            foreach (Customer c in listCustomer)
            {
                if (c.CustomerID == customerId)
                {
                    return c;
                }
            }
            return null;
        }

        public ObservableCollection<Customer> GetAllCustomers()
        {
            return listCustomer;

        }
    }
}
