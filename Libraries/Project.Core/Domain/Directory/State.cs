using System;

namespace Project.Core.Domain.Directory
{
    public class State : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int TIN { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
