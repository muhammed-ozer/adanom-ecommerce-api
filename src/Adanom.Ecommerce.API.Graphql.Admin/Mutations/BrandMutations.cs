using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType("Mutation")]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class BrandMutations
    {
        #region CreateBrandAsync

        [GraphQLDescription("Creates a brand")]
        public async Task<BrandResponse?> CreateBrandAsync(
            CreateBrandRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateBrand(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
