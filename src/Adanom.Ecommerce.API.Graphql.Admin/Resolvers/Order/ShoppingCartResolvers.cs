namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ShoppingCartResponse))]
    public sealed class ShoppingCartResolvers
    {
        #region GetTotalAsync

        public async Task<decimal> GetTotalAsync(
           [Parent] ShoppingCartResponse shoppingCartResponse,
           [Service] IMediator mediator)
        {
            var total = await mediator.Send(new GetShoppingCartTotal(shoppingCartResponse.Id));

            return total;
        }

        #endregion

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

        #region GetUserAsync

        public async Task<UserResponse?> GetUserAsync(
           [Parent] ShoppingCartResponse shoppingCartResponse,
           [Service] IMediator mediator)
        {
            var user = await mediator.Send(new GetUser(shoppingCartResponse.UserId));

            return user;
        }

        #endregion
    }
}
