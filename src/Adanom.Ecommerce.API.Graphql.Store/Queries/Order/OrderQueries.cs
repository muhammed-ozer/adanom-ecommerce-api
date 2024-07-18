namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class OrderQueries
    {
        #region GetOrderAsync

        [GraphQLDescription("Gets an order")]
        public async Task<OrderResponse?> GetOrderAsync(
            long id,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetOrder(identity, id);

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
