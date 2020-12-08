using Project.Core.Domain.Catalog;
using Project.Core.Domain.Customers;
using System;

namespace Project.Core.Domain.Orders
{
    public class ShoppingCart : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

        #region Navigational Properties
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }

        #endregion
    }
}
