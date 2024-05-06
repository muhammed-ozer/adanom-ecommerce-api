using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateProductTag : IRequest<ProductTagResponse?>
    {
        #region Ctor

        public CreateProductTag(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public string Value { get; set; } = null!;

        #endregion
    }
}