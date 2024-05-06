namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType("Query")]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductSpecificationAttributeQueries
    {
        #region GetProductSpecificationAttributeAsync

        [GraphQLDescription("Gets product specification attribute")]
        public async Task<ProductSpecificationAttributeResponse?> GetProductSpecificationAttributeAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetProductSpecificationAttribute(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetProductSpecificationAttributesAsync

        [GraphQLDescription("Gets product specification attributes")]
        public async Task<PaginatedData<ProductSpecificationAttributeResponse>> GetProductSpecificationAttributesAsync(
            GetProductSpecificationAttributesFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetProductSpecificationAttributes(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
