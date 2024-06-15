namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateProductSKURequest
    {
        public string Code { get; set; } = null!;

        public int StockQuantity { get; set; }

        public StockUnitType StockUnitType { get; set; }

        public string? Barcodes { get; set; }

        public bool IsEligibleToInstallment { get; set; }

        public byte MaximumInstallmentCount { get; set; }

        public CreateProductPriceRequest CreateProductPriceRequest { get; set; } = null!;
    }
}