using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductCategoryMutations
    {
        #region CreateProductCategoryAsync

        [GraphQLDescription("Creates a product category")]
        public async Task<ProductCategoryResponse?> CreateProductCategoryAsync(
            CreateProductCategoryRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProductCategory(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductCategoryAsync

        [GraphQLDescription("Updates a product category")]
        public async Task<ProductCategoryResponse?> UpdateProductCategoryAsync(
            UpdateProductCategoryRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductCategory(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteProductCategoryAsync

        [GraphQLDescription("Deletes a product category")]
        public async Task<bool> DeleteProductCategoryAsync(
            DeleteProductCategoryRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteProductCategory(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearProductCategoriesCacheAsync

        [GraphQLDescription("Clears product category cache")]
        public async Task<bool> ClearProductCategoriesCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<ProductCategoryResponse>());

            return true;
        }

        #endregion
    }
}
