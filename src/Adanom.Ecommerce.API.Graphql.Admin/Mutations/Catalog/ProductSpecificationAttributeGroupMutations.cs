using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductSpecificationAttributeGroupMutations
    {
        #region CreateProductSpecificationAttributeGroupAsync

        [GraphQLDescription("Creates a product specification attribute group")]
        public async Task<ProductSpecificationAttributeGroupResponse?> CreateProductSpecificationAttributeGroupAsync(
            CreateProductSpecificationAttributeGroupRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProductSpecificationAttributeGroup(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductSpecificationAttributeGroupAsync

        [GraphQLDescription("Updates a product specification attribute group")]
        public async Task<ProductSpecificationAttributeGroupResponse?> UpdateProductSpecificationAttributeGroupAsync(
            UpdateProductSpecificationAttributeGroupRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductSpecificationAttributeGroup(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteProductSpecificationAttributeGroupAsync

        [GraphQLDescription("Deletes a product specification attribute group")]
        public async Task<bool> DeleteProductSpecificationAttributeGroupAsync(
            DeleteProductSpecificationAttributeGroupRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteProductSpecificationAttributeGroup(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearProductSpecificationAttributeGroupsCacheAsync

        [GraphQLDescription("Clears product specification attribute group cache")]
        public async Task<bool> ClearProductSpecificationAttributeGroupsCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<ProductSpecificationAttributeGroupResponse>());

            return true;
        }

        #endregion
    }
}
