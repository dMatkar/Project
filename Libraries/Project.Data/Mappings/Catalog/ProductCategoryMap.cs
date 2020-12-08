using Project.Core.Domain.Catalog;

namespace Project.Data.Mappings.Catalog
{
    public class ProductCategoryMap : ProjectEntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMap()
        {
            HasKey(pc => pc.Id);
            ToTable("ProductCategoryMapping");

            HasRequired(pc => pc.Product)
                .WithMany(p=>p.Categories)
                .HasForeignKey(pc => pc.ProductId);

            HasRequired(pc => pc.Category)
                .WithMany()
                .HasForeignKey(pc => pc.CategoryId);
        }
    }
}
