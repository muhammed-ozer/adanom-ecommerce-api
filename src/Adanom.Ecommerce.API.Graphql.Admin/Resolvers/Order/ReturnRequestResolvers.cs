namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ReturnRequestResponse))]
    public sealed class ReturnRequestResolvers
    {
        #region GetItemsAsync

        public async Task<IEnumerable<ReturnRequestItemResponse>> GetItemsAsync(
           [Parent] ReturnRequestResponse returnRequestResponse,
           [Service] IMediator mediator)
        {
            var returnRequestItems = await mediator.Send(new GetReturnRequestItems(new GetReturnRequestItemsFilter()
            {
                ReturnRequestId = returnRequestResponse.Id
            }));

            return returnRequestItems;
        }

        #endregion

        #region GetReturnRequestStatusTypeAsync

        public async Task<ReturnRequestStatusTypeResponse> GetReturnRequestStatusTypeAsync(
           [Parent] ReturnRequestResponse returnRequestResponse,
           [Service] IMediator mediator)
        {
            var returnRequestStatusType = await mediator.Send(new GetReturnRequestStatusType(returnRequestResponse.ReturnRequestStatusType.Key));

            return returnRequestStatusType;
        }

        #endregion

        #region GetDeliveryTypeAsync

        public async Task<DeliveryTypeResponse> GetDeliveryTypeAsync(
           [Parent] ReturnRequestResponse returnRequestResponse,
           [Service] IMediator mediator)
        {
            var deliveryType = await mediator.Send(new GetDeliveryType(returnRequestResponse.DeliveryType.Key));

            return deliveryType;
        }

        #endregion

        #region GetOrderAsync

        public async Task<OrderResponse?> GetOrderAsync(
           [Parent] ReturnRequestResponse returnRequestResponse,
           [Service] IMediator mediator)
        {
            var order = await mediator.Send(new GetOrder(returnRequestResponse.OrderId));

            return order;
        }

        #endregion
    }
}
