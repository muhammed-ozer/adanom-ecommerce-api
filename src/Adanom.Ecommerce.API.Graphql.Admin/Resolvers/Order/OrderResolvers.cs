namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
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

        #region GetShippingAddressAsync

        public async Task<ShippingAddressResponse?> GetShippingAddressAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var shippingAddress = await mediator.Send(new GetShippingAddress(orderResponse.ShippingAddressId, true));

            return shippingAddress;
        }

        #endregion

        #region GetBillingAddressAsync

        public async Task<BillingAddressResponse?> GetBillingAddressAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            if (orderResponse.BillingAddressId == null)
            {
                return null;
            }

            var billingAddress = await mediator.Send(new GetBillingAddress(orderResponse.BillingAddressId.Value, true));

            return billingAddress;
        }

        #endregion
    }
}
