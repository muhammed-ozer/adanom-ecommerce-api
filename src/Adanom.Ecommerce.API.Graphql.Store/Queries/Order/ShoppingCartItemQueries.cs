namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class ShoppingCartItemQueries
    {
        #region GetShoppingCartItemsAsync

        [GraphQLDescription("Gets shopping cart items")]
        public async Task<IEnumerable<ShoppingCartItemResponse>> GetShoppingCartItemsAsync(
            GetShoppingCartItemsFilter filter,
            [Service] IMediator mediator)
        {
            var command = new GetShoppingCartItems(filter);

            return await mediator.Send(command);
        }

        #endregion

        #region GetShoppingCartItemsCountAsync

        [GraphQLDescription("Gets shopping cart items count")]
        public async Task<int> GetShoppingCartItemsCountAsync(
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetShoppingCartItemsCount(identity);

            return await mediator.Send(command);
        }

        #endregion
    }
}
