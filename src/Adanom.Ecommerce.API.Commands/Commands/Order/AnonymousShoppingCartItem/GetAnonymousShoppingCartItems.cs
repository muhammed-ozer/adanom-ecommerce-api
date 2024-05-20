namespace Adanom.Ecommerce.API.Commands
{
    public class GetAnonymousShoppingCartItems : IRequest<IEnumerable<AnonymousShoppingCartItemResponse>>
    {
        #region Ctor

        public GetAnonymousShoppingCartItems(GetAnonymousShoppingCartItemsFilter filter)
        {
            Filter = filter;
        }

        #endregion

        #region Properties

        public GetAnonymousShoppingCartItemsFilter Filter { get; set; }

        #endregion
    }
}
