using Project.Core.Domain.Customers;

namespace Project.Data.Mappings.Customers
{
    public class CustomerMap : ProjectEntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            HasKey(c => c.Id);
            ToTable("Customers");

            Property(c => c.FirstName).HasMaxLength(200);
            Property(c => c.LastName).HasMaxLength(200);
            Property(c => c.MobileNumber).HasMaxLength(20);
        }
    }
}
