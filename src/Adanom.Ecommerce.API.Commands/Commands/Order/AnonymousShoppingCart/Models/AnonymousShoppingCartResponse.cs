namespace Adanom.Ecommerce.API.Commands.Models
{
    public class AnonymousShoppingCartResponse : BaseResponseEntity<Guid>
    {
        public AnonymousShoppingCartResponse()
        {
            Items = new List<AnonymousShoppingCartItemResponse>();
        }

        public decimal Total { get; set; }

        public DateTime LastModifiedAtUtc { get; set; }

        public ICollection<AnonymousShoppingCartItemResponse> Items { get; set; }
    }
}