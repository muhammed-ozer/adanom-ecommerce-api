using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetShoppingCart : IRequest<ShoppingCartResponse?>
    {
        #region Ctor

        public GetShoppingCart(long id, bool updateItems)
        {
            Id = id;
            UpdateItems = updateItems;
        }

        public GetShoppingCart(Guid userId, bool updateItems)
        {
            UserId = userId;
            UpdateItems = updateItems;
        }

        public GetShoppingCart(ClaimsPrincipal identity, bool updateItems)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            UpdateItems = updateItems;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        public Guid? UserId { get; set; }

        public ClaimsPrincipal? Identity { get; }

        public bool UpdateItems { get; }

        #endregion
    }
}
