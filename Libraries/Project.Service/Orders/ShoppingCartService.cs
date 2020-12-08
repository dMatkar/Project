using Project.Core;
using Project.Core.Caching;
using Project.Core.Data;
using Project.Core.Domain.Orders;
using System;
using System.Linq;

namespace Project.Service.Orders
{
    public class ShoppingCartService : IShoppingCartService
    {
        #region Constants

        private const string SHOPPING_CART_ITEMS_BY_CUSTOMERID = "project.shopping.cart.id.{0}";
        private const string SHOPPING_CART_REMOVE_PATTERN_BY_ID = "project.shopping.cart.id";

        #endregion

        #region Fields

        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructor

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, ICacheManager cacheManager)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public void DeleteItem(ShoppingCart shoppingCart)
        {
            if (shoppingCart is null)
                throw new ArgumentNullException("shoppingCart");

            _shoppingCartRepository.Insert(shoppingCart);
            _cacheManager.RemoveByPattern(string.Format(SHOPPING_CART_REMOVE_PATTERN_BY_ID, shoppingCart.CustomerId));
        }

        public ShoppingCart GetCartItem(int Id,int customerId)
        {
            var query = _shoppingCartRepository.Table;
            return query.Where(item => item.Id == Id && item.CustomerId == customerId).FirstOrDefault();
        }

        public IPagedList<ShoppingCart> GetAllItems(int customerId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from item in _shoppingCartRepository.AsNoTracking
                        where item.CustomerId == customerId && !item.Customer.Deleted
                        select item;

            query = query.OrderByDescending(item => item.CreatedDateTime);
            return new PagedList<ShoppingCart>(query, pageIndex, pageSize);
        }

        public void InsertItem(ShoppingCart shoppingCart)
        {
            if (shoppingCart is null)
                throw new ArgumentException("shoppingCart");

            _shoppingCartRepository.Insert(shoppingCart);
        }

        public void UpdateItem(ShoppingCart shoppingCart)
        {
            if (shoppingCart is null)
                throw new ArgumentException("shoppingCart");

            _shoppingCartRepository.Update(shoppingCart);
        }

        public ShoppingCart IsItemExists(int customerId, int productId)
        {
            var query = _shoppingCartRepository.Table;
            query = query.Where(cust => cust.CustomerId == customerId && cust.ProductId == productId);
            return query.FirstOrDefault();
        }

        public virtual decimal GetCartTotal(int customerId)
        {
            var query = _shoppingCartRepository.Table;
            query = query.Where(cust => cust.CustomerId == customerId);
            return query.Sum(item => item.TotalAmount);
        }

        #endregion
    }
}
