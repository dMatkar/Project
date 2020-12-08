using Project.Core.Domain.Localization;

namespace Project.Data.Mappings.Localization
{
    public partial class LocalStringResourceMap : ProjectEntityTypeConfiguration<LocaleStringResource>
    {
        public LocalStringResourceMap()
        {
            HasKey(lsr => lsr.Id);

            Property(lsr => lsr.ResourceName).IsRequired().HasMaxLength(200);
            Property(lsr => lsr.ResourceValue).IsRequired();

            #region Navigation Property

            HasRequired(lsr => lsr.Language)
                .WithMany(l => l.LocaleStringResources)
                .HasForeignKey(lsr => lsr.LanguageId);

            #endregion
        }
    }
}
