namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ReturnRequestItemResponse))]
    public sealed class ReturnRequestItemResolvers
    {
        #region GetReturnRequestAsync

        public async Task<ReturnRequestResponse?> GetReturnRequestAsync(
           [Parent] ReturnRequestItemResponse returnRequestItemResponse,
           [Service] IMediator mediator)
        {
            var returnRequest = await mediator.Send(new GetReturnRequest(returnRequestItemResponse.ReturnRequestId));

            return returnRequest;
        }

        #endregion

        #region GetOrderItemAsync

        public async Task<OrderItemResponse?> GetOrderItemAsync(
           [Parent] ReturnRequestItemResponse returnRequestItemResponse,
           [Service] IMediator mediator)
        {
            var orderItem = await mediator.Send(new GetOrderItem(returnRequestItemResponse.OrderItemId));

            return orderItem;
        }

        #endregion
    }
}
