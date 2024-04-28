namespace Adanom.Ecommerce.API.Data.Models
{
    public class ShoppingCartItem : BaseEntity<long>
    {
        public long ShoppingCartId { get; set; }

        public long ProductId { get; set; }

        public long ProductSKUId { get; set; }

        public int Amount { get; set; }

        public DateTime LastModifiedAtUtc { get; set; }

        public Product Product { get; set; } = null!;

        public ProductSKU ProductSKU { get; set; } = null!;

        public ShoppingCart ShoppingCart { get; set; } = null!;
    }
}
