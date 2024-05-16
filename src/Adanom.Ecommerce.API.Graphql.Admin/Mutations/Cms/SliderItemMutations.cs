using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class SliderItemMutations
    {
        #region CreateSliderItemAsync

        [GraphQLDescription("Creates a slider item")]
        public async Task<SliderItemResponse?> CreateSliderItemAsync(
            CreateSliderItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateSliderItem(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateSliderItemAsync

        [GraphQLDescription("Updates a slider item")]
        public async Task<bool> UpdateSliderItemAsync(
            UpdateSliderItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateSliderItem(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteSliderItemAsync

        [GraphQLDescription("Deletes a slider item")]
        public async Task<bool> DeleteSliderItemAsync(
            DeleteSliderItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteSliderItem(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
