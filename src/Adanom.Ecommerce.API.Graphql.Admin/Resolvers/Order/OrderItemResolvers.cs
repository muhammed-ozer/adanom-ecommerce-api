namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
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

        #region GetProductSKUAsync

        public async Task<ProductSKUResponse?> GetProductSKUAsync(
           [Parent] OrderItemResponse orderItemResponse,
           [Service] IMediator mediator)
        {
            var productSKU = await mediator.Send(new GetProductSKU(orderItemResponse.ProductSKUId));

            return productSKU;
        }

        #endregion
    }
}
