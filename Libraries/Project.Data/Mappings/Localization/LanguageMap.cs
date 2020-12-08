using Project.Core.Domain.Localization;

namespace Project.Data.Mappings.Localization
{
    public class LanguageMap : ProjectEntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            HasKey(l => l.Id);

            Property(l => l.Name).IsRequired().HasMaxLength(200);
            Property(l => l.LanguageCulture).IsRequired().HasMaxLength(20);
            Property(l => l.FlagImageFileName).HasMaxLength(50);
        }
    }
}
