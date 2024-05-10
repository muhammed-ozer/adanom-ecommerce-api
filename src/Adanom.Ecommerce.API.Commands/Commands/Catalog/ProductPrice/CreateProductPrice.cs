using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateProductPrice : IRequest<ProductPriceResponse?>
    {
        #region Ctor

        public CreateProductPrice(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long TaxCategoryId { get; set; }

        public decimal OriginalPrice { get; set; }

        #endregion
    }
}