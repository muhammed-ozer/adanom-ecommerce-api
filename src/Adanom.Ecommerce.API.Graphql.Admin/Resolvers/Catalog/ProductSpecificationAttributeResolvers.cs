namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ProductSpecificationAttributeResponse))]
    public sealed class ProductSpecificationAttributeResolvers
    {
        #region GetProductSpecificationAttributeGroupAsync

        public async Task<ProductSpecificationAttributeGroupResponse?> GetProductSpecificationAttributeGroupAsync(
           [Parent] ProductSpecificationAttributeResponse productSpecificationAttributeResponse,
           [Service] IMediator mediator)
        {
            var productSpecificationAttributeGroup = await mediator.Send(new GetProductSpecificationAttributeGroup(
                productSpecificationAttributeResponse.ProductSpecificationAttributeGroupId));

            return productSpecificationAttributeGroup;
        }

        #endregion
    }
}
