namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateAnonymousShoppingCartItemResponse
    {
        public AnonymousShoppingCartResponse? AnonymousShoppingCart { get; set; }

        public bool IsSuccess { get; set; }
    }
}