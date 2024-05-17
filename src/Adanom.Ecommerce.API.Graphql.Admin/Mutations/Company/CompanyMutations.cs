using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class CompanyMutations
    {
        #region UpdateCompanyAsync

        [GraphQLDescription("Updates a company")]
        public async Task<bool> UpdateCompanyAsync(
            UpdateCompanyRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateCompany(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region ClearCompaniesCacheAsync

        [GraphQLDescription("Clears company cache")]
        public async Task<bool> ClearCompaniesCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<CompanyResponse>());

            return true;
        }

        #endregion

    }
}
