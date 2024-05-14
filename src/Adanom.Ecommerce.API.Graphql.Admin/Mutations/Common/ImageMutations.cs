using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType("Mutation")]
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
    }
}
