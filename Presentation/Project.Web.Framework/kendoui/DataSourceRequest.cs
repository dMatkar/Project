namespace Project.Web.Framework.kendoui
{
    public class DataSourceRequest
    {
        #region Properties

        public int Page { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Constructor

        public DataSourceRequest()
        {
            Page = 1;
            PageSize = 10;
        }

        #endregion
    }
}
