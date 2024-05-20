namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(OrderResponse))]
    public sealed class OrderResolvers
    {
        #region GetUserAsync

        public async Task<UserResponse?> GetUserAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var user = await mediator.Send(new GetUser(orderResponse.UserId));

            return user;
        }

        #endregion

        #region GetOrderStatusTypeAsync

        public async Task<OrderStatusTypeResponse?> GetOrderStatusTypeAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var orderStatusType = await mediator.Send(new GetOrderStatusType(orderResponse.OrderStatusType!.Key));

            return orderStatusType;
        }

        #endregion

        #region GetDeliveryTypeAsync

        public async Task<DeliveryTypeResponse?> GetDeliveryTypeAsync(
           [Parent] OrderResponse orderResponse,
           [Service] IMediator mediator)
        {
            var deliveryType = await mediator.Send(new GetDeliveryType(orderResponse.DeliveryType!.Key));

            return deliveryType;
        }

        #endregion
    }
}
