using Project.Core.Domain.Common;
using System;

namespace Project.Core.Domain.Customers
{
    public partial class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public bool IsTaxExempt { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

        #region Navigational Properties

        public virtual Address Address { get; set; }

        #endregion
    }
}
