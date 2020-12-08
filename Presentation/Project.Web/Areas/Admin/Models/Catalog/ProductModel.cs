using Project.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Project.Web.Areas.Admin.Models.Catalog
{
    public partial class ProductModel : BaseEntityModel
    {
        #region Constructor

        public ProductModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableMeasurements = new List<SelectListItem>();
            CategoryIds =new List<int>();
        }

        #endregion

        #region Properties

        #region Produdct Info

        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public IList<int> CategoryIds { get; set; }
        public int MeasurementId { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public string AdminComment { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableMeasurements { get; set; }
 
        #endregion

        #region Prices
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal ProductCost { get; set; }
        public bool IsTaxExempt { get; set; }

        #endregion

        #region Inventory

        public int InventoryMethodId { get; set; }
        public long StockQuantity { get; set; }
        public int LowStockActivityId { get; set; }
        public int NotifyAdminForQuantityBelow { get; set; }

        #endregion

        #endregion
    }
}
