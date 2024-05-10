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
    }
}
