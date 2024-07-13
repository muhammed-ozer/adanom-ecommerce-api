namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ShippingProviderQueries
    {
        #region GetShippingProviderAsync

        [AllowAnonymous]
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

        [AllowAnonymous]
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
