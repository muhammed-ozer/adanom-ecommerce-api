using Adanom.Ecommerce.API.Graphql.DataLoaders;

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
           [Service] ProductByIdDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var product = await dataLoader.LoadAsync(orderItemResponse.ProductId);

            return mapper.Map<ProductResponse>(product);
        }

        #endregion
    }
}
