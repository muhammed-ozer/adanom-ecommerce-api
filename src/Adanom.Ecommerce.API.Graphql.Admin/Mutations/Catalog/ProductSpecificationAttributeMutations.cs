using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductSpecificationAttributeMutations
    {
        #region CreateProductSpecificationAttributeAsync

        [GraphQLDescription("Creates a product specification attribute")]
        public async Task<ProductSpecificationAttributeResponse?> CreateProductSpecificationAttributeAsync(
            CreateProductSpecificationAttributeRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProductSpecificationAttribute(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductSpecificationAttributeAsync

        [GraphQLDescription("Updates a product specification attribute")]
        public async Task<ProductSpecificationAttributeResponse?> UpdateProductSpecificationAttributeAsync(
            UpdateProductSpecificationAttributeRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductSpecificationAttribute(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteProductSpecificationAttributeAsync

        [GraphQLDescription("Deletes a product specification attribute")]
        public async Task<bool> DeleteProductSpecificationAttributeAsync(
            DeleteProductSpecificationAttributeRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteProductSpecificationAttribute(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearProductSpecificationAttributesCacheAsync

        [GraphQLDescription("Clears product specification attribute cache")]
        public async Task<bool> ClearProductSpecificationAttributesCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<ProductSpecificationAttributeResponse>());

            return true;
        }

        #endregion
    }
}
