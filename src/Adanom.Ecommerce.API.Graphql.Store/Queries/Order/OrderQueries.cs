namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class OrderQueries
    {
        #region GetOrderByIdAsync

        [GraphQLDescription("Gets an order by id")]
        public async Task<OrderResponse?> GetOrderByIdAsync(
            long id,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetOrder(identity, id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetOrderByOrderNumberAsync

        [GraphQLDescription("Gets an order by order number")]
        public async Task<OrderResponse?> GetOrderByOrderNumberAsync(
            string orderNumber,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetOrder(identity, orderNumber);

            return await mediator.Send(command);
        }

        #endregion

        #region GetOrdersAsync

        [GraphQLDescription("Gets orders")]
        public async Task<PaginatedData<OrderResponse>> GetOrdersAsync(
            GetOrdersFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetOrders(filter, paginationRequest);
            var userId = identity.GetUserId();

            if (command.Filter == null)
            {
                command.Filter = new GetOrdersFilter();
            }

            command.Filter.UserId = userId;

            return await mediator.Send(command);
        }

        #endregion
    }
}
