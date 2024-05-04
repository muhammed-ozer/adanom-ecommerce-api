using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateBrand : IRequest<BrandResponse?>
    {
        #region Ctor

        public UpdateBrand(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }

        #endregion
    }
}