namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class OrderStatusTypeQueries
    {
        #region GetOrderStatusTypesAsync

        [GraphQLDescription("Gets order status types")]
        public async Task<IEnumerable<OrderStatusTypeResponse>> GetOrderStatusTypesAsync([Service] IMediator mediator)
        {
            var command = new GetOrderStatusTypes();

            return await mediator.Send(command);
        }

        #endregion
    }
}
