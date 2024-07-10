using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class PickUpStoreMutations
    {
        #region CreatePickUpStoreAsync

        [GraphQLDescription("Creates a pick up store")]
        public async Task<PickUpStoreResponse?> CreatePickUpStoreAsync(
            CreatePickUpStoreRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreatePickUpStore(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdatePickUpStoreAsync

        [GraphQLDescription("Updates a pick up store")]
        public async Task<bool> UpdatePickUpStoreAsync(
            UpdatePickUpStoreRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdatePickUpStore(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdatePickUpStoreLogoAsync

        [GraphQLDescription("Updates a pick up store logo")]
        public async Task<bool> UpdatePickUpStoreLogoAsync(
            UpdatePickUpStoreLogoRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdatePickUpStoreLogo(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeletePickUpStoreAsync

        [GraphQLDescription("Deletes a pick up store")]
        public async Task<bool> DeletePickUpStoreAsync(
            DeletePickUpStoreRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeletePickUpStore(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearPickUpStoresCacheAsync

        [GraphQLDescription("Clears pick up store cache")]
        public async Task<bool> ClearPickUpStoresCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<PickUpStoreResponse>());

            return true;
        }

        #endregion
    }
}
