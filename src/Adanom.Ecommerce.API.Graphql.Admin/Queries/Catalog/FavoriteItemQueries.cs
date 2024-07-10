namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
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
