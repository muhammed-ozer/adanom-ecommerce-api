namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class StockUnitTypeResponse
    {
        public StockUnitTypeResponse(StockUnitType key)
        {
            Key = key;
        }

        public StockUnitType Key { get; }

        public string Name { get; set; } = null!;
    }
}