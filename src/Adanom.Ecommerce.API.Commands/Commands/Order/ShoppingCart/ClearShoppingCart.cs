using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class ClearShoppingCart : IRequest<bool>
    {
        #region Ctor

        public ClearShoppingCart(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        #endregion
    }
}