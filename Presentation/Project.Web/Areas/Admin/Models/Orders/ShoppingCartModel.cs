using Project.Web.Framework.Models;

namespace Project.Web.Areas.Admin.Models.Orders
{
    public class ShoppingCartModel : BaseEntityModel
    {
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public string Measurement { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }

        public string QuantityWithMeasurement  
        {
            get => $"{Quantity} {Measurement}";
        }
    }
}
