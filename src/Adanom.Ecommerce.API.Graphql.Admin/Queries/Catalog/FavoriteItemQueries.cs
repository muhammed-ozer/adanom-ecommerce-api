namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class FavoriteItemQueries
    {
        #region GetFavoriteItemsAsync

        [GraphQLDescription("Gets favorite items")]
        public async Task<PaginatedData<FavoriteItemResponse>> GetFavoriteItemsAsync(
            GetFavoriteItemsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetFavoriteItems(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
