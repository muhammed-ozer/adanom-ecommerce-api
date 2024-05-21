namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class OrderQueries
    {
        #region GetOrderAsync

        [GraphQLDescription("Gets an order")]
        public async Task<OrderResponse?> GetOrderAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetOrder(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetOrdersAsync

        [GraphQLDescription("Gets orders")]
        public async Task<PaginatedData<OrderResponse>> GetOrdersAsync(
            GetOrdersFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetOrders(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
