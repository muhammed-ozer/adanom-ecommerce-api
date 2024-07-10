namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
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
    }
}
