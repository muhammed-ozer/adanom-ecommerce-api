using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteShoppingCartItem : IRequest<bool>
    {
        #region Ctor

        public DeleteShoppingCartItem(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        #endregion
    }
}