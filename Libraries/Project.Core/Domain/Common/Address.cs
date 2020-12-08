using System;

namespace Project.Core.Domain.Common
{
    public class Address : BaseEntity
    {
        public int? StateProvinceId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string ZipPostalCode { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
