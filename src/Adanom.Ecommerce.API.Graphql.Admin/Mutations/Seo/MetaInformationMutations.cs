using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class MetaInformationMutations
    {
        #region UpdateMetaInformationAsync

        [GraphQLDescription("Updates a meta information")]
        public async Task<MetaInformationResponse?> UpdateMetaInformationAsync(
            UpdateMetaInformationRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateMetaInformation(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
