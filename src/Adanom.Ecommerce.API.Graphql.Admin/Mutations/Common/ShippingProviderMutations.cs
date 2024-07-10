using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ShippingProviderMutations
    {
        #region CreateShippingProviderAsync

        [GraphQLDescription("Creates a shipping provider")]
        public async Task<ShippingProviderResponse?> CreateShippingProviderAsync(
            CreateShippingProviderRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateShippingProvider(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateShippingProviderAsync

        [GraphQLDescription("Updates a shipping provider")]
        public async Task<bool> UpdateShippingProviderAsync(
            UpdateShippingProviderRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateShippingProvider(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateShippingProviderLogoAsync

        [GraphQLDescription("Updates a shipping provider logo")]
        public async Task<bool> UpdateShippingProviderLogoAsync(
            UpdateShippingProviderLogoRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateShippingProviderLogo(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteShippingProviderAsync

        [GraphQLDescription("Deletes a shipping provider")]
        public async Task<bool> DeleteShippingProviderAsync(
            DeleteShippingProviderRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteShippingProvider(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearShippingProvidersCacheAsync

        [GraphQLDescription("Clears shipping provider cache")]
        public async Task<bool> ClearShippingProvidersCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<ShippingProviderResponse>());

            return true;
        }

        #endregion
    }
}
