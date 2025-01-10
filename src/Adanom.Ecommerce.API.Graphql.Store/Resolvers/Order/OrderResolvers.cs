namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(OrderResponse))]
    public sealed class OrderResolvers
    {
        #region GetItemsAsync

        public async Task<IEnumerable<OrderItemResponse>> GetItemsAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var orderItems = await mediator.Send(new GetOrderItems(new GetOrderItemsFilter()
            {
                OrderId = orderResponse.Id
            }));

            return orderItems;
        }

        #endregion

        #region GetUserAsync

        public async Task<UserResponse?> GetUserAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var user = await mediator.Send(new GetUser(orderResponse.UserId));

            return user;
        }

        #endregion

        #region GetOrderPaymentAsync

        public async Task<OrderPaymentResponse?> GetOrderPaymentAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var orderPayment = await mediator.Send(new GetOrderPayment(orderResponse.OrderNumber));

            return orderPayment;
        }

        #endregion

        #region GetOrderStatusTypeAsync

        public async Task<OrderStatusTypeResponse> GetOrderStatusTypeAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var orderStatusType = await mediator.Send(new GetOrderStatusType(orderResponse.OrderStatusType.Key));

            return orderStatusType;
        }

        #endregion

        #region GetDeliveryTypeAsync

        public async Task<DeliveryTypeResponse> GetDeliveryTypeAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var deliveryType = await mediator.Send(new GetDeliveryType(orderResponse.DeliveryType.Key));

            return deliveryType;
        }

        #endregion

        #region GetOrderShippingAddressAsync

        public async Task<OrderShippingAddressResponse?> GetOrderShippingAddressAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var orderShippingAddress = await mediator.Send(new GetOrderShippingAddress(orderResponse.OrderShippingAddressId));

            return orderShippingAddress;
        }

        #endregion

        #region GetOrderBillingAddressAsync

        public async Task<OrderBillingAddressResponse?> GetOrderBillingAddressAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            if (orderResponse.OrderBillingAddressId == null)
            {
                return null;
            }

            var orderBillingAddress = await mediator.Send(new GetOrderBillingAddress(orderResponse.OrderBillingAddressId.Value));

            return orderBillingAddress;
        }

        #endregion

        #region GetPickUpStoreAsync

        public async Task<PickUpStoreResponse?> GetPickUpStoreAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            if (orderResponse.PickUpStoreId == null)
            {
                return null;
            }

            var pickUpStore = await mediator.Send(new GetPickUpStore(orderResponse.PickUpStoreId.Value));

            return pickUpStore;
        }

        #endregion

        #region GetShippingProviderAsync

        public async Task<ShippingProviderResponse?> GetShippingProviderAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            if (orderResponse.ShippingProviderId == null)
            {
                return null;
            }

            var shippingProvider = await mediator.Send(new GetShippingProvider(orderResponse.ShippingProviderId.Value));

            return shippingProvider;
        }

        #endregion

        #region GetLocalDeliveryProviderAsync

        public async Task<LocalDeliveryProviderResponse?> GetLocalDeliveryProviderAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            if (orderResponse.LocalDeliveryProviderId == null)
            {
                return null;
            }

            var localDeliveryProvider = await mediator.Send(new GetLocalDeliveryProvider(orderResponse.LocalDeliveryProviderId.Value));

            return localDeliveryProvider;
        }

        #endregion
    }
}
