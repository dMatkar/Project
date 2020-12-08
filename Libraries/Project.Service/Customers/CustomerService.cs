using Project.Core;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Service.Customers
{
    public class CustomerService : ICustomerService
    {
        #region Constants

        private const string SERACH_CUSTOMER_BY_NAME = "project.customer.customers";
        private const string CUSTOMER_REMOVE_PATTERN = "project.customer";

        #endregion

        #region Fields

        private readonly IRepository<Customer> _customerRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructor

        public CustomerService(IRepository<Customer> customerRepository, ICacheManager cacheManager)
        {
            _customerRepository = customerRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public void DeleteCustomer(Customer customer)
        {
            if (customer is null)
                throw new ArgumentNullException("customer");

              customer.Deleted = true;
            _customerRepository.Delete(customer);
            _cacheManager.RemoveByPattern(CUSTOMER_REMOVE_PATTERN);
        }

        public virtual IPagedList<Customer> GetAllCustomers(string firstName = null, string lastName = null,
            string email = null, string mobileNumber = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerRepository.Table;
            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(x => x.FirstName.Contains(firstName));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(x => x.LastName.Contains(lastName));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(x => x.Email.Contains(email));

            if (!string.IsNullOrWhiteSpace(mobileNumber))
                query = query.Where(x => x.MobileNumber.Contains(mobileNumber));

            query = query.Where(x => !x.Deleted);
            query = query.OrderByDescending(x => x.CreatedOnUtc);
            return new PagedList<Customer>(query, pageIndex, pageSize);
        }

        public Customer GetCustomerById(int customerId)
        {
            if (customerId == 0)
                return null;

            return _customerRepository.GetById(customerId);
        }

        public void InsertCustomer(Customer customer)
        {
            if (customer is null)
                throw new ArgumentNullException("customer");

            _customerRepository.Insert(customer);
            _cacheManager.RemoveByPattern(CUSTOMER_REMOVE_PATTERN);
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer is null)
                throw new ArgumentNullException("customer");

            _customerRepository.Update(customer);
            _cacheManager.RemoveByPattern(CUSTOMER_REMOVE_PATTERN);
        }

        /// <summary>
        /// return true all customers that are active or inactive not deleted.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsCustomerExists(int Id)
        {
            var query = _customerRepository.AsNoTracking;
            query = query.Where(customer => customer.Id == Id && !customer.Deleted);
            return query.Count() > 0;
        }

        /// <summary>
        /// only active customer will displayed.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDictionary<int, string> SearchCustomerByName(string name)
        {
            var dic = _cacheManager.Get<IDictionary<int, string>>(SERACH_CUSTOMER_BY_NAME, () =>
              {
                  var query = from item in _customerRepository.AsNoTracking
                              where item.Active
                              select new
                              {
                                  item.Id,
                                  item.FirstName,
                                  item.LastName
                              };

                  return query.ToDictionary(c => c.Id, v => $"{v.FirstName} {v.LastName}");
              });
            return dic.Where(c => c.Value.IndexOf(name,StringComparison.InvariantCultureIgnoreCase) > -1).ToDictionary(c => c.Key, v => v.Value);
        }

        #endregion
    }
}
