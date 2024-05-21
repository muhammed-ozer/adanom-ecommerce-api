namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductSKUResponse : BaseResponseEntity<long>
    {
        public long ProductId { get; set; }

        public long? ProductAttributeId { get; set; }

        public long ProductPriceId { get; set; }

        public string Code { get; set; } = null!;

        public int StockQuantity { get; set; }

        public StockUnitTypeResponse StockUnitType { get; set; } = null!;

        public string? Barcodes { get; set; }

        public bool IsEligibleToInstallment { get; set; }

        public byte MaximumInstallmentCount { get; set; }

        public ProductPriceResponse? ProductPrice { get; set; }

        public ProductAttributeResponse? ProductAttribute { get; set; }
    }
}