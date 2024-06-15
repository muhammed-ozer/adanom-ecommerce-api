using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class ImageMutations
    {
        #region CreateImageAsync

        [GraphQLDescription("Creates an image")]
        public async Task<ImageResponse?> CreateImageAsync(
            CreateImageRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateImage(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateImageAsync

        [GraphQLDescription("Updates an image")]
        public async Task<bool> UpdateImageAsync(
            UpdateImageRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateImage(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteImageAsync

        [GraphQLDescription("Deletes an image")]
        public async Task<bool> DeleteImageAsync(
            DeleteImageRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteImage(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
