namespace Adanom.Ecommerce.API.Commands
{
    public class GetShoppingCartItems : IRequest<IEnumerable<ShoppingCartItemResponse>>
    {
        #region Ctor

        public GetShoppingCartItems(GetShoppingCartItemsFilter filter)
        {
            Filter = filter;
        }

        #endregion

        #region Properties

        public GetShoppingCartItemsFilter Filter { get; set; }

        #endregion
    }
}
