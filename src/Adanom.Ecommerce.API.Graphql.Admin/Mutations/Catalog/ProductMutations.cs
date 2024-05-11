using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType("Mutation")]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ProductMutations
    {
        #region CreateProductAsync

        [GraphQLDescription("Creates a product")]
        public async Task<ProductResponse?> CreateProductAsync(
            CreateProductRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateProduct(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductNameAsync

        [GraphQLDescription("Updates a product name")]
        public async Task<ProductResponse?> UpdateProductNameAsync(
            UpdateProductNameRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductName(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductDescriptionAsync

        [GraphQLDescription("Updates a product description")]
        public async Task<ProductResponse?> UpdateProductDescriptionAsync(
            UpdateProductDescriptionRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductDescription(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateProductBrandAsync

        [GraphQLDescription("Updates a product brand")]
        public async Task<ProductResponse?> UpdateProductBrandAsync(
            UpdateProductBrandRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateProductBrand(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
