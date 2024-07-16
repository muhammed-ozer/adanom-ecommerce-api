namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateShoppingCartItemRequest
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }
    }
}