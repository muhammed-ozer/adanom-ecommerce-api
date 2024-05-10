using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType("Mutation")]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
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
    }
}
