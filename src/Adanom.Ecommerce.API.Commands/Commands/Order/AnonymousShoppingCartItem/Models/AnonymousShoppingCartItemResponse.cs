namespace Adanom.Ecommerce.API.Commands.Models
{
    public class AnonymousShoppingCartItemResponse : BaseResponseEntity<long>
    {
        public Guid AnonymousShoppingCartId { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public ProductResponse? Product { get; set; }

        public AnonymousShoppingCartResponse? AnonymousShoppingCart { get; set; }
    }
}