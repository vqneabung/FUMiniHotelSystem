using BussinessObjects;
using System.Collections.ObjectModel;

namespace Services
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        Customer GetCustomerById(int customerId);
        ObservableCollection<Customer> GetAllCustomers();
        Customer GetCustomerByEmail(string customerEmail);

    }
}
