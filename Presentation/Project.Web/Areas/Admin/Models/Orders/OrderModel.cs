using Project.Web.Framework.Models;
using System;

namespace Project.Web.Areas.Admin.Models.Orders
{
    public class OrderModel : BaseEntityModel
    {
        public OrderModel()
        {
            CartItems = new CartItems();
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal PaymentDue  { get; set; }

        public CartItems CartItems  { get; set; }
    }
}
