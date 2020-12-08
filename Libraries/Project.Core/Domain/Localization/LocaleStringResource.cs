namespace Project.Core.Domain.Localization
{
    public class LocaleStringResource : BaseEntity
    {
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }

        #region Navigation Properties

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        #endregion
    }
}
