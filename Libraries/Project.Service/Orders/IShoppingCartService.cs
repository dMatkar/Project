using Project.Core;
using Project.Core.Domain.Orders;

namespace Project.Service.Orders
{
    public interface IShoppingCartService
    {
        IPagedList<ShoppingCart> GetAllItems(int customerId, int pageIndex = 0, int pageSize = int.MaxValue);
        ShoppingCart IsItemExists(int customerId, int productId);
        ShoppingCart GetCartItem(int Id, int customerId);
        decimal GetCartTotal(int customerId);
        void InsertItem(ShoppingCart shoppingCart);
        void DeleteItem(ShoppingCart shoppingCart);
        void UpdateItem(ShoppingCart shoppingCart);
    }
}
