using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteShoppingCart : IRequest<bool>
    {
        #region Ctor

        public DeleteShoppingCart(long id)
        {
            Id = id;
        }

        public DeleteShoppingCart(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public long Id { get; }

        public ClaimsPrincipal? Identity { get; }

        #endregion
    }
}