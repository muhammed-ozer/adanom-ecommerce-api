namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class OrderPaymentQueries
    {
        #region GetOrderPaymentAsync

        [GraphQLDescription("Gets an order payment")]
        public async Task<OrderPaymentResponse?> GetOrderPaymentAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetOrderPayment(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetOrderPaymentsAsync

        [GraphQLDescription("Gets order payments")]
        public async Task<PaginatedData<OrderPaymentResponse>> GetOrderPaymentsAsync(
            GetOrderPaymentsFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetOrderPayments(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
