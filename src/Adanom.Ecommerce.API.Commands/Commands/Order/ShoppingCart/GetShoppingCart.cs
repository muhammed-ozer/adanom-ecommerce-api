using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetShoppingCart : IRequest<ShoppingCartResponse?>
    {
        #region Ctor

        public GetShoppingCart(long id)
        {
            Id = id;
        }

        public GetShoppingCart(Guid userId)
        {
            UserId = userId;
        }

        public GetShoppingCart(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public Guid? UserId { get; set; }

        public ClaimsPrincipal? Identity { get; }

        #endregion
    }
}
