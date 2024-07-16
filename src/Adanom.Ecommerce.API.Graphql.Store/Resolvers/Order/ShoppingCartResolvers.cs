namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(ShoppingCartResponse))]
    public sealed class ShoppingCartResolvers
    {
        #region GetItemsAsync

        public async Task<IEnumerable<ShoppingCartItemResponse>> GetItemsAsync(
           [Parent] ShoppingCartResponse shoppingCartResponse,
           [Service] IMediator mediator)
        {
            var shoppingCartItems = await mediator.Send(new GetShoppingCartItems(new GetShoppingCartItemsFilter()
            {
                ShoppingCartId = shoppingCartResponse.Id
            }));

            return shoppingCartItems;
        }

        #endregion
    }
}
