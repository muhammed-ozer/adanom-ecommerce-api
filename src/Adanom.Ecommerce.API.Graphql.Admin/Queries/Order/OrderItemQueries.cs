namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class OrderItemQueries
    {
        #region GetOrderItemsAsync

        [GraphQLDescription("Gets order items")]
        public async Task<IEnumerable<OrderItemResponse>> GetOrderItemsAsync(
            GetOrderItemsFilter filter,
            [Service] IMediator mediator)
        {
            var command = new GetOrderItems(filter);

            return await mediator.Send(command);
        }

        #endregion
    }
}
