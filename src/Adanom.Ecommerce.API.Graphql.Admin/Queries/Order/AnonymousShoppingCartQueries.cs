namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class AnonymousShoppingCartQueries
    {
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
