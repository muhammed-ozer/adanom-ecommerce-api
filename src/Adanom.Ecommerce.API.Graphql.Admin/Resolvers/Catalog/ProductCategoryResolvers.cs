namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ProductCategoryResponse))]
    public sealed class ProductCategoryResolvers
    {
        #region GetParentAsync

        public async Task<ProductCategoryResponse?> GetParentAsync(
           [Parent] ProductCategoryResponse productCategoryResponse,
           [Service] IMediator mediator)
        {
            if (productCategoryResponse.ParentId == null)
            {
                return null;
            }

            var parent = await mediator.Send(new GetProductCategory(productCategoryResponse.ParentId.Value));

            return parent;
        }

        #endregion

    }
}
