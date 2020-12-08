using Project.Core.Domain.Catalog;

namespace Project.Data.Mappings.Catalog
{
    public class MeasurementMap : ProjectEntityTypeConfiguration<Measurement>
    {
        public MeasurementMap()
        {
            HasKey(m => m.Id);
            ToTable("Measurements");

            Property(m => m.Name).HasMaxLength(100);
            Property(m => m.ShortName).HasMaxLength(20);
            Property(m => m.AdminComment).HasMaxLength(500);
        }
    }
}
