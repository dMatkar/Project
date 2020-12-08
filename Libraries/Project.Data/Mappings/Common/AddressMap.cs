using Project.Core.Domain.Common;

namespace Project.Data.Mappings.Common
{
    public class AddressMap : ProjectEntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            HasKey(a => a.Id);
            ToTable("Addresses");

            Property(a => a.Address1).HasMaxLength(1000);
            Property(a => a.Address2).HasMaxLength(1000);
            Property(a => a.Address3).HasMaxLength(1000);
            Property(a => a.City).HasMaxLength(100);
            Property(a => a.ZipPostalCode).HasMaxLength(10);
        }
    }
}
