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

            var productPrice = await mediator.Send(new GetProductPrice(productSKUResponse.ProductPriceId));

            return productPrice;
        }

        #endregion

        #region GetProductAttributeAsync

        public async Task<ProductAttributeResponse?> GetProductAttributeAsync(
           [Parent] ProductSKUResponse productSKUResponse,
           [Service] IMediator mediator)
        {
            if (productSKUResponse == null)
            {
                return null;
            }

            if (productSKUResponse.ProductAttributeId == null)
            {
                return null;
            }

            var productAttribute = await mediator.Send(new GetProductAttribute(productSKUResponse.ProductAttributeId.Value));

            return productAttribute;
        }

        #endregion
    }
}
