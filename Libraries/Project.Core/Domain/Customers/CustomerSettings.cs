using Project.Core.Configuration;

namespace Project.Core.Domain.Customers
{
    public class CustomerSettings : ISettings
    {
        public bool CustomerNameEnabled { get; set; }
        public bool CustomerEmailEnabled { get; set; }
    }
}
