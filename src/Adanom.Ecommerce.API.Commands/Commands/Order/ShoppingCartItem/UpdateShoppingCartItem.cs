using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateShoppingCartItem : IRequest<bool>
    {
        #region Ctor

        public UpdateShoppingCartItem(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public long ProductId { get; set; }

        public int Amount { get; set; }

        #endregion
    }
}