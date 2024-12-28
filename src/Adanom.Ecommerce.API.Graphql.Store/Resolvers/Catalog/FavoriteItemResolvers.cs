using Adanom.Ecommerce.API.Graphql.DataLoaders;

namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(FavoriteItemResponse))]
    public sealed class FavoriteItemResolvers
    {
        #region GetProductAsync

        public async Task<ProductResponse?> GetProductAsync(
           [Parent] FavoriteItemResponse favoriteItemResponse,
           [Service] ProductByIdDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var product = await dataLoader.LoadAsync(favoriteItemResponse.ProductId);

            return mapper.Map<ProductResponse>(product);
        }

        #endregion

        #region GetUserAsync

        public async Task<UserResponse?> GetUserAsync(
           [Parent] FavoriteItemResponse favoriteItemResponse,
           [Service] IMediator mediator)
        {
            var user = await mediator.Send(new GetUser(favoriteItemResponse.UserId));

            return user;
        }

        #endregion
    }
}
