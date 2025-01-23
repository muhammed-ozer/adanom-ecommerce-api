namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
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

        #region GetOrderAsync

        public async Task<OrderResponse?> GetOrderAsync(
           [Parent] ReturnRequestResponse returnRequestResponse,
           [Service] IMediator mediator)
        {
            var order = await mediator.Send(new GetOrder(returnRequestResponse.OrderId));

            return order;
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

        #region GetShippingProviderAsync

        public async Task<ShippingProviderResponse?> GetShippingProviderAsync(
           [Parent] ReturnRequestResponse returnRequestResponse,
           [Service] IMediator mediator)
        {
            if (!returnRequestResponse.ShippingProviderId.HasValue)
            {
                return null;
            }

            var shippingProvider = await mediator.Send(new GetShippingProvider(returnRequestResponse.ShippingProviderId.Value));

            return shippingProvider;
        }

        #endregion

        #region GetPickUpStoreAsync

        public async Task<PickUpStoreResponse?> GetPickUpStoreAsync(
           [Parent] ReturnRequestResponse returnRequestResponse,
           [Service] IMediator mediator)
        {
            if (!returnRequestResponse.PickUpStoreId.HasValue)
            {
                return null;
            }

            var pickUpStore = await mediator.Send(new GetPickUpStore(returnRequestResponse.PickUpStoreId.Value));

            return pickUpStore;
        }

        #endregion

        #region GetLocalDeliveryProviderAsync

        public async Task<LocalDeliveryProviderResponse?> GetLocalDeliveryProviderAsync(
           [Parent] ReturnRequestResponse returnRequestResponse,
           [Service] IMediator mediator)
        {
            if (!returnRequestResponse.LocalDeliveryProviderId.HasValue)
            {
                return null;
            }

            var localDeliveryProvider = await mediator.Send(new GetLocalDeliveryProvider(returnRequestResponse.LocalDeliveryProviderId.Value));

            return localDeliveryProvider;
        }

        #endregion
    }
}
