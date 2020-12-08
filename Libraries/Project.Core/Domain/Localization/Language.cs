using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Project.Core.Domain.Localization
{
    public class Language : BaseEntity
    {
        #region Fields

        private ICollection<LocaleStringResource> _localeStringResources;

        #endregion

        public string Name  { get; set; }
        public string LanguageCulture { get; set; }
        public string FlagImageFileName { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }

        #region Navigation Properties

        public virtual ICollection<LocaleStringResource> LocaleStringResources
        {
            get => _localeStringResources ?? (_localeStringResources = new Collection<LocaleStringResource>());
            set => _localeStringResources = value;
        }
       
        #endregion
    }
}
