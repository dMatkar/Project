using System;

namespace Project.Core.Domain.Catalog
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public bool Deleted { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
