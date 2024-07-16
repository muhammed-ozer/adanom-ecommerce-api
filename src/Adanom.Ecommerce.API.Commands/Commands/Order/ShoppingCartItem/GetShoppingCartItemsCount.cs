using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetShoppingCartItemsCount : IRequest<int>
    {
        #region Ctor

        public GetShoppingCartItemsCount(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        #endregion
    }
}
