namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(OrderPaymentResponse))]
    public sealed class OrderPaymentResolvers
    {
        #region GetOrderPaymentTypeAsync

        public async Task<OrderPaymentTypeResponse> GetOrderPaymentTypeAsync(
           [Parent] OrderPaymentResponse orderPaymentResponse,
           [Service] IMediator mediator)
        {
            var orderPaymentType = await mediator.Send(new GetOrderPaymentType(orderPaymentResponse.OrderPaymentType.Key));

            return orderPaymentType;
        }

        #endregion
    }
}
