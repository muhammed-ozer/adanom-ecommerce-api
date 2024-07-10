namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ShoppingCartQueries
    {
        #region GetShoppingCartAsync

        [GraphQLDescription("Gets shopping cart")]
        public async Task<ShoppingCartResponse?> GetShoppingCartAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetShoppingCart(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetShoppingCartsAsync

        [GraphQLDescription("Gets shopping carts")]
        public async Task<PaginatedData<ShoppingCartResponse>> GetShoppingCartsAsync(
            GetShoppingCartsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetShoppingCarts(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion

        #region GetShoppingCartsCountAsync

        [GraphQLDescription("Gets shopping carts count")]
        public async Task<int> GetShoppingCartsCountAsync([Service] IMediator mediator)
        {
            var command = new GetShoppingCartsCount();

            return await mediator.Send(command);
        }

        #endregion
    }
}
