namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ProductSpecificationAttributeGroupResponse))]
    public sealed class ProductSpecificationAttributeGroupResolvers
    {
        #region GetProductSpecificationAttributesAsync

        public async Task<IEnumerable<ProductSpecificationAttributeResponse>> GetProductSpecificationAttributesAsync(
           [Parent] ProductSpecificationAttributeGroupResponse productSpecificationAttributeGroupResponse,
           [Service] IMediator mediator)
        {
            var filter = new GetProductSpecificationAttributesFilter()
            {
                ProductSpecificationAttributeGroupId = productSpecificationAttributeGroupResponse.Id
            };

            var productSpecificationAttributes = await mediator.Send(new GetProductSpecificationAttributes(filter));

            return productSpecificationAttributes.Rows;
        }

        #endregion
    }
}
