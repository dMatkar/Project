namespace Project.Core.Domain.Catalog
{
    public class ProductCategory : BaseEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int DisplayOrder  { get; set; }
        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
    }
}
