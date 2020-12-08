using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Project.Core.Domain.Catalog
{
    public class Product : BaseEntity
    {
        #region Fields

        private ICollection<ProductCategory> _productCategories;

        #endregion

        #region Properties

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public bool Published { get; set; }
        public int MeasurementId  { get; set; }
        public bool Deleted { get; set; }
        public int DisplayOrder { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ProductCost { get; set; }
        public bool IsTaxExempt { get; set; }
        public int InventoryMethodId { get; set; }
        public long StockQuantity { get; set; }
        public int LowStockActivityId { get; set; }
        public int NotifyAdminForQuantityBelow { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public string AdminComment { get; set; }

        public LowStockActivity LowStockActivity
        {
            get => (LowStockActivity)LowStockActivityId;
            set => LowStockActivityId = (int)value;
        }
        public ManageInventoryMethod ManageInventoryMethod
        {
            get => (ManageInventoryMethod)InventoryMethodId;
            set => InventoryMethodId = (int)value;
        }

        #region Navigatinal Properties

        public virtual ICollection<ProductCategory> Categories
        {
            get => _productCategories ?? (_productCategories = new Collection<ProductCategory>());
            set => _productCategories = value;
        }

        public virtual Measurement Measurement { get; set; }

        #endregion

        #endregion
    }
}
