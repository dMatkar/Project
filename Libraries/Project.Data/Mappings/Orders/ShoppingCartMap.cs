using Project.Core.Domain.Orders;

namespace Project.Data.Mappings.Orders
{
    public class ShoppingCartMap : ProjectEntityTypeConfiguration<ShoppingCart>
    {
        public ShoppingCartMap()
        {
            HasKey(cart => cart.Id);
            ToTable("ShoppingCarts");

            HasRequired(cart => cart.Customer)
                .WithMany()
                .HasForeignKey(cart => cart.CustomerId);

            HasRequired(cart => cart.Product)
                .WithMany()
                .HasForeignKey(cart => cart.ProductId);
        }
    }
}
