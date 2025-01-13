using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    [Transactional]
    public class CreateBrand : IRequest<BrandResponse?>
    {
        #region Ctor

        public CreateBrand(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }

        #endregion
    }
}