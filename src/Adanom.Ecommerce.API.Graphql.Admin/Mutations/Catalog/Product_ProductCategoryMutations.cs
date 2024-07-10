using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class Product_ProductCategoryMutations
    {
        #region CreateProduct_ProductCategoryAsync

        [GraphQLDescription("Creates a product productCategory")]
        public async Task<bool> CreateProduct_ProductCategoryAsync(
            CreateProduct_ProductCategoryRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProduct_ProductCategory(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteProduct_ProductCategoryAsync

        [GraphQLDescription("Deletes a product productCategory")]
        public async Task<bool> DeleteProduct_ProductCategoryAsync(
            DeleteProduct_ProductCategoryRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteProduct_ProductCategory(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
