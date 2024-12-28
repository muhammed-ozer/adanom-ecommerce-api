using Adanom.Ecommerce.API.Graphql.DataLoaders;

namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ProductSKUResponse))]
    public sealed class ProductSKUResolvers
    {
        #region GetStockUnitTypeAsync

        public async Task<StockUnitTypeResponse> GetStockUnitTypeAsync(
           [Parent] ProductSKUResponse productSKUResponse,
           [Service] IMediator mediator)
        {
            var stockUnitType = await mediator.Send(new GetStockUnitType(productSKUResponse.StockUnitType.Key));

            return stockUnitType;
        }

        #endregion

        #region GetProductPriceAsync

        public async Task<ProductPriceResponse?> GetProductPriceAsync(
           [Parent] ProductSKUResponse productSKUResponse,
           [Service] ProductPriceByIdDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            if (productSKUResponse == null)
            {
                return null;
            }

            var productPrice = await dataLoader.LoadAsync(productSKUResponse.ProductPriceId);

            return mapper.Map<ProductPriceResponse>(productPrice);
        }

        #endregion
    }
}
