namespace Adanom.Ecommerce.API.Commands
{
    public class CalculateShoppingCartItemTotalsForCheckoutAndOrder : IRequest<CalculateShoppingCartItemTotalsForCheckoutAndOrderResponse?>
    {
        #region Ctor

        public CalculateShoppingCartItemTotalsForCheckoutAndOrder(ShoppingCartItemResponse shoppingCartItem, UserResponse user)
        {
            ShoppingCartItem = shoppingCartItem;
            User = user;
        }

        #endregion

        #region Properties

        public ShoppingCartItemResponse ShoppingCartItem { get; }

        public UserResponse User { get; }

        #endregion
    }
}