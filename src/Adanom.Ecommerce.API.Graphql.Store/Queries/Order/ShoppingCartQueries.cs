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
            var command = new GetShoppingCart(identity, true);

            return await mediator.Send(command);
        }

        #endregion
    }
}
