namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class AnonymousShoppingCartQueries
    {
        #region GetAnonymousShoppingCartAsync

        [GraphQLDescription("Gets anonymous shopping cart")]
        public async Task<AnonymousShoppingCartResponse?> GetAnonymousShoppingCartAsync(
            Guid id,
            [Service] IMediator mediator)
        {
            var command = new GetAnonymousShoppingCart(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetAnonymousShoppingCartItemsCountAsync

        [GraphQLDescription("Gets anonymous shopping cart items count")]
        public async Task<int> GetAnonymousShoppingCartItemsCountAsync(
            Guid id,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetAnonymousShoppingCartItemsCount(id);

            return await mediator.Send(command);
        }

        #endregion
    }
}
