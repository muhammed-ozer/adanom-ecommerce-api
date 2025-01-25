using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateProduct_ProductSKU : IRequest<bool>
    {
        #region Ctor

        public CreateProduct_ProductSKU(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductId { get; set; }

        public long ProductSKUId { get; set; }

        #endregion
    }
}