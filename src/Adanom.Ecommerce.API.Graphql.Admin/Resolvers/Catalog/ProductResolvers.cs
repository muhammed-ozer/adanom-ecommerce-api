namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ProductResponse))]
    public sealed class ProductResolvers
    {
        #region GetBrandAsync

        public async Task<BrandResponse?> GetBrandAsync(
           [Parent] ProductResponse productResponse,
           [Service] IMediator mediator)
        {
            if (productResponse.BrandId == null)
            {
                return null;
            }

            var brand = await mediator.Send(new GetBrandById(productResponse.BrandId.Value));

            return brand;
        }

        #endregion
    }
}
