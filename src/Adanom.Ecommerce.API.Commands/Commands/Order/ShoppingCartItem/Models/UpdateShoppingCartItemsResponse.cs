namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateShoppingCartItemsResponse
    {
        public bool HasNoItem { get; set; }

        public bool HasPriceChanges { get; set; }

        public bool HasProductDeleted { get; set; }

        public bool HasStocksChanges { get; set; }
    }
}