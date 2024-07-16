using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateShoppingCart : IRequest<ShoppingCartResponse?>
    {
        #region Ctor

        public CreateShoppingCart(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        #endregion
    }
}