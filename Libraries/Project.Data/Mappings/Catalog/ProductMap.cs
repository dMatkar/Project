using Project.Core.Domain.Catalog;

namespace Project.Data.Mappings.Catalog
{
    public class ProductMap : ProjectEntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(product => product.Id);
            ToTable("Products");

            Property(product => product.Name).IsRequired().HasMaxLength(100);
            Property(product => product.ShortDescription).HasMaxLength(1000);
            Property(product => product.AdminComment).HasMaxLength(1000);
            Property(product => product.Price).HasPrecision(18, 2);
            Property(product => product.OldPrice).HasPrecision(18, 2);
            Property(product => product.ProductCost).HasPrecision(18, 2);

            HasRequired(measurement => measurement.Measurement)
                .WithMany()
                .HasForeignKey(measurement => measurement.MeasurementId);

            Ignore(product => product.LowStockActivity);
            Ignore(product => product.ManageInventoryMethod);
        }
    }
}
