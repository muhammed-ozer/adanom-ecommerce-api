namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ProductSKUResponse))]
    public sealed class ProductSKUResolvers
    {
        #region GetStockUnitTypeAsync

        public async Task<StockUnitTypeResponse?> GetStockUnitTypeAsync(
           [Parent] ProductSKUResponse productSKUResponse,
           [Service] IMediator mediator)
        {
            if (productSKUResponse == null || productSKUResponse.StockUnitType == null)
            {
                return null;
            }

            var stockUnitType = await mediator.Send(new GetStockUnitType(productSKUResponse.StockUnitType.Key));

            return stockUnitType;
        }

        #endregion

        #region GetProductPriceAsync

        public async Task<ProductPriceResponse?> GetProductPriceAsync(
           [Parent] ProductSKUResponse productSKUResponse,
           [Service] IMediator mediator)
        {
            if (productSKUResponse == null)
            {
                return null;
            }

            var productPrice = await mediator.Send(new GetProductPrice(productSKUResponse.Id));

            return productPrice;
        }

        #endregion
    }
}
