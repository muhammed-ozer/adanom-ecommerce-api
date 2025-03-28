namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
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

        #region GetOrdersCountAsync

        [GraphQLDescription("Gets orders count")]
        public async Task<int> GetOrdersCountAsync(
            GetOrdersCountFilter? filter,
            [Service] IMediator mediator)
        {
            var command = new GetOrdersCount(filter);

            return await mediator.Send(command);
        }

        #endregion

        #region GetOrdersTotalGrandTotalAsync

        [GraphQLDescription("Gets orders total grand total")]
        public async Task<decimal> GetOrdersTotalGrandTotalAsync(
            GetOrdersTotalGrandTotalFilter? filter,
            [Service] IMediator mediator)
        {
            var command = new GetOrdersTotalGrandTotal(filter);

            return await mediator.Send(command);
        }

        #endregion
    }
}
