using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductBrand : IRequest<ProductResponse?>
    {
        #region Ctor

        public UpdateProductBrand(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public long? BrandId { get; set; }

        #endregion
    }
}