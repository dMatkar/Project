using Project.Core.Domain.Configuration;

namespace Project.Data.Mappings.Configuration
{
    public class SettingMap : ProjectEntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            ToTable("Settings");
            HasKey(s => s.Id);

            Property(s => s.Name).IsRequired().HasMaxLength(200);
            Property(s => s.Value).IsRequired().HasMaxLength(2000);
        }
    }
}
