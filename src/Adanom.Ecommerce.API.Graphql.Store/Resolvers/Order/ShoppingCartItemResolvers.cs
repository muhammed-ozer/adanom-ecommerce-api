using Adanom.Ecommerce.API.Graphql.DataLoaders;

namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(ShoppingCartItemResponse))]
    public sealed class ShoppingCartItemResolvers
    {
        #region GetShoppingCartAsync

        public async Task<ShoppingCartResponse?> GetShoppingCartAsync(
           [Parent] ShoppingCartItemResponse shoppingCartItemResponse,
           [Service] IMediator mediator)
        {
            var shoppingCart = await mediator.Send(new GetShoppingCart(shoppingCartItemResponse.ShoppingCartId, true));

            return shoppingCart;
        }

        #endregion

        #region GetProductAsync

        public async Task<ProductResponse?> GetProductAsync(
           [Parent] ShoppingCartItemResponse shoppingCartItemResponse,
           [Service] ProductByIdDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var product = await dataLoader.LoadAsync(shoppingCartItemResponse.ProductId);

            return mapper.Map<ProductResponse>(product);
        }

        #endregion
    }
}
