namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(ProductPriceResponse))]
    public sealed class ProductPriceResolvers
    {
        #region GetTaxCategoryAsync

        public async Task<TaxCategoryResponse?> GetTaxCategoryAsync(
           [Parent] ProductPriceResponse productPriceResponse,
           [Service] IMediator mediator)
        {
            var taxCategory = await mediator.Send(new GetTaxCategory(productPriceResponse.TaxCategoryId));

            return taxCategory;
        }

        #endregion
    }
}
