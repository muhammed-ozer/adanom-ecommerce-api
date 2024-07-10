using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class TaxAdministrationMutations
    {
        #region CreateTaxAdministrationAsync

        [GraphQLDescription("Creates a tax administration")]
        public async Task<TaxAdministrationResponse?> CreateTaxAdministrationAsync(
            CreateTaxAdministrationRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateTaxAdministration(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateTaxAdministrationAsync

        [GraphQLDescription("Updates a tax administration")]
        public async Task<bool> UpdateTaxAdministrationAsync(
            UpdateTaxAdministrationRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateTaxAdministration(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteTaxAdministrationAsync

        [GraphQLDescription("Deletes a tax administration")]
        public async Task<bool> DeleteTaxAdministrationAsync(
            DeleteTaxAdministrationRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteTaxAdministration(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
