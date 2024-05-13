using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateProductSKU : IRequest<ProductSKUResponse?>
    {
        #region Ctor

        public CreateProductSKU(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductId { get; set; }

        public string Code { get; set; } = null!;

        public int StockQuantity { get; set; }

        public StockUnitType StockUnitType { get; set; }

        public string? Barcodes { get; set; }

        public bool IsEligibleToInstallment { get; set; }

        public byte MaximumInstallmentCount { get; set; }

        public CreateProductPriceRequest CreateProductPriceRequest { get; set; } = null!;

        public CreateProductAttributeRequest? CreateProductAttributeRequest { get; set; }

        #endregion
    }
}