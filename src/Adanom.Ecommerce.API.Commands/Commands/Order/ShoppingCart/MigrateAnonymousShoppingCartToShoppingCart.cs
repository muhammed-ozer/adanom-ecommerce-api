using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class MigrateAnonymousShoppingCartToShoppingCart : IRequest<bool>
    {
        #region Ctor

        public MigrateAnonymousShoppingCartToShoppingCart(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public Guid AnonymousShoppingCartId { get; set; }

        #endregion
    }
}