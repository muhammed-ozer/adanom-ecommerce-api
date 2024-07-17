namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateShoppingCartItemsResponse
    {
        public bool HasPriceChanges { get; set; }

        public bool HasProductDeleted { get; set; }
    }
}