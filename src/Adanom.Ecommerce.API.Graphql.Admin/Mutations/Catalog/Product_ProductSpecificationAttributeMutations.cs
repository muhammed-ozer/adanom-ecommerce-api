using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class Product_ProductSpecificationAttributeMutations
    {
        #region CreateProduct_ProductSpecificationAttributeAsync

        [GraphQLDescription("Creates a product productSpecificationAttribute")]
        public async Task<bool> CreateProduct_ProductSpecificationAttributeAsync(
            CreateProduct_ProductSpecificationAttributeRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProduct_ProductSpecificationAttribute(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteProduct_ProductSpecificationAttributeAsync

        [GraphQLDescription("Deletes a product productSpecificationAttribute")]
        public async Task<bool> DeleteProduct_ProductSpecificationAttributeAsync(
            DeleteProduct_ProductSpecificationAttributeRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteProduct_ProductSpecificationAttribute(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
