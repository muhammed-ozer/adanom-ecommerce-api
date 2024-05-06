using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductTag : IRequest<ProductTagResponse?>
    {
        #region Ctor

        public UpdateProductTag(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string Value { get; set; } = null!;

        #endregion
    }
}