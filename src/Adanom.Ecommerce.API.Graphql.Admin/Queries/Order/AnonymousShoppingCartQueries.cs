namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
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

        #region GetAnonymousShoppingCartsAsync

        [GraphQLDescription("Gets anonymous shopping carts")]
        public async Task<PaginatedData<AnonymousShoppingCartResponse>> GetAnonymousShoppingCartsAsync(
            GetAnonymousShoppingCartsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetAnonymousShoppingCarts(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion

        #region GetAnonymousShoppingCartsCountAsync

        [GraphQLDescription("Gets anonymous shopping carts count")]
        public async Task<int> GetAnonymousShoppingCartsCountAsync([Service] IMediator mediator)
        {
            var command = new GetAnonymousShoppingCartsCount();

            return await mediator.Send(command);
        }

        #endregion
    }
}
