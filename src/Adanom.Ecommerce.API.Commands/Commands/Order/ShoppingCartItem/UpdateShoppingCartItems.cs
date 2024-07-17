using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateShoppingCartItems : IRequest<UpdateShoppingCartItemsResponse>
    {
        #region Ctor

        public UpdateShoppingCartItems(ClaimsPrincipal identity)
        {
            Identity = identity;
        }

        public UpdateShoppingCartItems(long shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }

        public UpdateShoppingCartItems(Guid userId)
        {
            UserId = userId;
        }

        #endregion

        #region Properties

        public ClaimsPrincipal? Identity { get; }

        public long? ShoppingCartId { get; set; }

        public Guid? UserId { get; set; }

        #endregion
    }
}