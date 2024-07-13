namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class StockNotificationItemQueries
    {
        #region GetStockNotificationItemsAsync

        [GraphQLDescription("Gets stock notification items")]
        public async Task<PaginatedData<StockNotificationItemResponse>> GetStockNotificationItemsAsync(
            GetStockNotificationItemsFilter? filter,
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

            var command = new GetStockNotificationItems(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
