namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class ShippingProviderQueries
    {
        #region GetShippingProviderAsync

        [GraphQLDescription("Gets shipping provider")]
        public async Task<ShippingProviderResponse?> GetShippingProviderAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetShippingProvider(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetShippingProvidersAsync

        [GraphQLDescription("Gets shipping providers")]
        public async Task<PaginatedData<ShippingProviderResponse>> GetShippingProvidersAsync(
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetShippingProviders(paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
