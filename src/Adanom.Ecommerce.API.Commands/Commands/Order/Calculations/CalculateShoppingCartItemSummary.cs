namespace Adanom.Ecommerce.API.Commands
{
    public class CalculateShoppingCartItemSummary : IRequest<CalculateShoppingCartItemSummaryResponse?>
    {
        #region Ctor

        public CalculateShoppingCartItemSummary(ShoppingCartItemResponse shoppingCartItem, UserResponse user, OrderPaymentType? orderPaymentType = null)
        {
            ShoppingCartItem = shoppingCartItem;
            User = user;
            OrderPaymentType = orderPaymentType;
        }

        #endregion

        #region Properties

        public ShoppingCartItemResponse ShoppingCartItem { get; }

        public UserResponse User { get; }

        public OrderPaymentType? OrderPaymentType { get; set; }

        #endregion
    }
}