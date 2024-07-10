namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class StockNotificationItemQueries
    {
        #region GetStockNotificationItemsAsync

        [GraphQLDescription("Gets stock notification items")]
        public async Task<PaginatedData<StockNotificationItemResponse>> GetStockNotificationItemsAsync(
            GetStockNotificationItemsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetStockNotificationItems(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
