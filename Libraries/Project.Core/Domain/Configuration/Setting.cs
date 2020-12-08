namespace Project.Core.Domain.Configuration
{
    public class Setting : BaseEntity
    {
        #region Constructor

        public Setting()
        {
        }

        public Setting(string name, string value, int storeId = 0)
        {
            Name = name;
            Value = value;
            StoreId = storeId;
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public string Value { get; set; }
        public int StoreId { get; set; }

        #endregion
    }
}
