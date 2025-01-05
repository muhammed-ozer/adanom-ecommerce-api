namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class ShoppingCartQueries
    {
        #region GetShoppingCartAsync

        [GraphQLDescription("Gets shopping cart")]
        public async Task<ShoppingCartResponse?> GetShoppingCartAsync(
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetShoppingCart(identity, true, true, true);

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
