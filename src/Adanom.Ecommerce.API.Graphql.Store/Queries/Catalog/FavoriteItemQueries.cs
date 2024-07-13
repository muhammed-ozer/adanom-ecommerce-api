namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class FavoriteItemQueries
    {
        #region GetFavoriteItemsAsync

        [GraphQLDescription("Gets favorite items")]
        public async Task<PaginatedData<FavoriteItemResponse>> GetFavoriteItemsAsync(
            GetFavoriteItemsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var userId = identity.GetUserId();
            
            if (filter == null)
            {
                filter = new()
                {
                    UserId = userId,
                };
            }
            else
            {
                filter.UserId = userId;
            }

            var command = new GetFavoriteItems(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
