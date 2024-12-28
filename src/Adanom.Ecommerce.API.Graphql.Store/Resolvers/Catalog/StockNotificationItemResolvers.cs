using Adanom.Ecommerce.API.Graphql.DataLoaders;

namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(StockNotificationItemResponse))]
    public sealed class StockNotificationItemResolvers
    {
        #region GetProductAsync

        public async Task<ProductResponse?> GetProductAsync(
           [Parent] StockNotificationItemResponse stockNotificationItemResponse,
           [Service] ProductByIdDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var product = await dataLoader.LoadAsync(stockNotificationItemResponse.ProductId);

            return mapper.Map<ProductResponse>(product);
        }

        #endregion

        #region GetUserAsync

        public async Task<UserResponse?> GetUserAsync(
           [Parent] StockNotificationItemResponse stockNotificationItemResponse,
           [Service] IMediator mediator)
        {
            var user = await mediator.Send(new GetUser(stockNotificationItemResponse.UserId));

            return user;
        }

        #endregion
    }
}
