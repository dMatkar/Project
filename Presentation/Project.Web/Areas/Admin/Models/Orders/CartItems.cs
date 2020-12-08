using Project.Web.Framework.Models;

namespace Project.Web.Areas.Admin.Models.Orders
{
    public class CartItems : BaseEntityModel
    {
        public int ProductId { get; set; }
        public string ProductName  { get; set; }
        public decimal Price { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal CurrentStockQuantity { get; set; }
        public double Quantity { get; set; }
    }
    public class AddToCartModel  
    {
        public int Id  { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
