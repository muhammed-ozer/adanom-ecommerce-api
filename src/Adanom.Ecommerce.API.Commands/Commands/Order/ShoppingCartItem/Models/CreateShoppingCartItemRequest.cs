namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateShoppingCartItemRequest
    {
        public long ProductId { get; set; }

        public int Amount { get; set; }
    }
}