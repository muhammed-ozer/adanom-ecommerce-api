namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ShoppingCartItemResponse : BaseResponseEntity<long>
    {
        public long ShoppingCartId { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public DateTime LastModifiedAtUtc { get; set; }

        public ProductResponse? Product { get; set; }

        public ShoppingCartResponse? ShoppingCart { get; set; }
    }
}