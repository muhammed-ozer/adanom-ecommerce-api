namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class LocalDeliveryProviderMutations
    {
        #region CreateLocalDeliveryProviderAsync

        [GraphQLDescription("Creates a local delivery provider")]
        public async Task<LocalDeliveryProviderResponse?> CreateLocalDeliveryProviderAsync(
            CreateLocalDeliveryProviderRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateLocalDeliveryProvider(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateLocalDeliveryProviderAsync

        [GraphQLDescription("Updates a local delivery provider")]
        public async Task<bool> UpdateLocalDeliveryProviderAsync(
            UpdateLocalDeliveryProviderRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateLocalDeliveryProvider(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateLocalDeliveryProviderLogoAsync

        [GraphQLDescription("Updates a local delivery provider logo")]
        public async Task<bool> UpdateLocalDeliveryProviderLogoAsync(
            UpdateLocalDeliveryProviderLogoRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateLocalDeliveryProviderLogo(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteLocalDeliveryProviderAsync

        [GraphQLDescription("Deletes a local delivery provider")]
        public async Task<bool> DeleteLocalDeliveryProviderAsync(
            DeleteLocalDeliveryProviderRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteLocalDeliveryProvider(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearLocalDeliveryProvidersCacheAsync

        [GraphQLDescription("Clears local delivery provider cache")]
        public async Task<bool> ClearLocalDeliveryProvidersCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<LocalDeliveryProviderResponse>());

            return true;
        }

        #endregion
    }
}
