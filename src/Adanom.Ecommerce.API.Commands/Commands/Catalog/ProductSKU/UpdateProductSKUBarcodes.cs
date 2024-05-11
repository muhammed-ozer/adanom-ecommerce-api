using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductSKUBarcodes : IRequest<bool>
    {
        #region Ctor

        public UpdateProductSKUBarcodes(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string? Barcodes { get; set; }

        #endregion
    }
}