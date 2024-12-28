using Adanom.Ecommerce.API.Graphql.DataLoaders;

namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(AnonymousShoppingCartItemResponse))]
    public sealed class AnonymousShoppingCartItemResolvers
    {
        #region GetAnonymousShoppingCartAsync

        public async Task<AnonymousShoppingCartResponse?> GetAnonymousShoppingCartAsync(
           [Parent] AnonymousShoppingCartItemResponse anonymousShoppingCartItemResponse,
           [Service] IMediator mediator)
        {
            var anonymousShoppingCart = await mediator.Send(new GetAnonymousShoppingCart(anonymousShoppingCartItemResponse.AnonymousShoppingCartId));

            return anonymousShoppingCart;
        }

        #endregion

        #region GetProductAsync

        public async Task<ProductResponse?> GetProductAsync(
           [Parent] AnonymousShoppingCartItemResponse anonymousShoppingCartItemResponse,
           [Service] ProductByIdDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var product = await dataLoader.LoadAsync(anonymousShoppingCartItemResponse.ProductId);

            return mapper.Map<ProductResponse>(product);
        }

        #endregion
    }
}
