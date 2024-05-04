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

        #region UpdateBrandAsync

        [GraphQLDescription("Updates a brand")]
        public async Task<BrandResponse?> UpdateBrandAsync(
            UpdateBrandRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateBrand(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearBrandsCacheAsync

        [GraphQLDescription("Clears brand cache")]
        public async Task<bool> ClearBrandsCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<BrandResponse>());

            return true;
        }

        #endregion
    }
}
