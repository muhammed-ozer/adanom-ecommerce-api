namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType("Query")]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class TaxAdministrationQueries
    {
        #region GetTaxAdministrationAsync

        [GraphQLDescription("Gets a tax administration")]
        public async Task<TaxAdministrationResponse?> GetTaxAdministrationAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetTaxAdministration(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetTaxAdministrationsAsync

        [GraphQLDescription("Gets tax administrations")]
        public async Task<PaginatedData<TaxAdministrationResponse>> GetTaxAdministrationsAsync(
            GetTaxAdministrationsFilter filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetTaxAdministrations(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
