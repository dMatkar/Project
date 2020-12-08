using Project.Core;
using Project.Core.Domain.Customers;
using System.Collections.Generic;

namespace Project.Service.Customers
{
    public interface ICustomerService
    {
        IPagedList<Customer> GetAllCustomers(string firstName = null, string lastName = null,
            string email = null, string mobileNumber = null,
            int pageIndex = 0, int pageSize = int.MaxValue);
        IDictionary<int,string> SearchCustomerByName(string name);
        bool IsCustomerExists(int Id);
        void InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        Customer GetCustomerById(int customerId);
    }
}
