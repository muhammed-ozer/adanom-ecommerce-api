using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateShoppingCartItem : IRequest<bool>
    {
        #region Ctor

        public CreateShoppingCartItem(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        #endregion
    }
}