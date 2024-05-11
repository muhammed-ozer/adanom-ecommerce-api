namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateProductSKUStockRequest
    {
        public long Id { get; set; }

        public int StockQuantity { get; set; }

        public StockUnitType StockUnitType { get; set; }
    }
}