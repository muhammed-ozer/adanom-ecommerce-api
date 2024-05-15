using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType("Mutation")]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class Image_EntityMutations
    {
        #region UpdateImage_EntityAsync

        [GraphQLDescription("Updates an image entity")]
        public async Task<bool> UpdateImage_EntityAsync(
            UpdateImage_EntityRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateImage_Entity(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
