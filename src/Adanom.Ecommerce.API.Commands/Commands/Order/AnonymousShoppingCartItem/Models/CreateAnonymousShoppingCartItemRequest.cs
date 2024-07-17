namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateAnonymousShoppingCartItemRequest
    {
        public Guid? AnonymousShoppingCartId { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }
    }
}