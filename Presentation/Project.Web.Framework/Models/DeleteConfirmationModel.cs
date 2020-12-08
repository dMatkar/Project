namespace Project.Web.Framework.Models
{
    public class DeleteConfirmationModel
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string WindowId  { get; set; }
    }
}
