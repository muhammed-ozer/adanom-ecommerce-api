namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(OrderItemResponse))]
    public sealed class OrderItemResolvers
    {
        #region GetOrderAsync

        public async Task<OrderResponse?> GetOrderAsync(
           [Parent] OrderItemResponse orderItemResponse,
           [Service] IMediator mediator)
        {
            var order = await mediator.Send(new GetOrder(orderItemResponse.OrderId));

            return order;
        }

        #endregion

        #region GetProductAsync

        public async Task<ProductResponse?> GetProductAsync(
           [Parent] OrderItemResponse orderItemResponse,
           [Service] IMediator mediator)
        {
            var product = await mediator.Send(new GetProduct(orderItemResponse.ProductId));

            return product;
        }

        #endregion
    }
}
