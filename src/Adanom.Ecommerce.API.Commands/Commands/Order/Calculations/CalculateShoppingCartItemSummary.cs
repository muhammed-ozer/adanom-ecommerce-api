namespace Adanom.Ecommerce.API.Commands
{
    public class CalculateShoppingCartItemSummary : IRequest<CalculateShoppingCartItemSummaryResponse?>
    {
        #region Ctor

        public CalculateShoppingCartItemSummary(ShoppingCartItemResponse shoppingCartItem, UserResponse user)
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