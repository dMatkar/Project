using Project.Core.Domain.Catalog;

namespace Project.Data.Mappings.Catalog
{
    public class CategoryMap : ProjectEntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            HasKey(c => c.Id);
            ToTable("Categories");

            Property(c => c.Name).HasMaxLength(500);
            Property(c => c.ShortDescription).HasMaxLength(1000);
        }
    }
}
