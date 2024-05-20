namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ShoppingCartItemResponse : BaseResponseEntity<long>
    {
        public long ShoppingCartId { get; set; }

        public long ProductId { get; set; }

        public long ProductSKUId { get; set; }

        public int Amount { get; set; }

        public DateTime LastModifiedAtUtc { get; set; }

        public ProductResponse Product { get; set; } = null!;

        public ProductSKUResponse ProductSKU { get; set; } = null!;

        public ShoppingCartResponse ShoppingCart { get; set; } = null!;
    }
}