using System;

namespace Project.Core.Domain.Catalog
{
    public class Measurement : BaseEntity
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string AdminComment { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
