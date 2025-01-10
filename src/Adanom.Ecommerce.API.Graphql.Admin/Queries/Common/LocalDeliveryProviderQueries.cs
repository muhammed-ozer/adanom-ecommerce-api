namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class LocalDeliveryProviderQueries
    {
        #region GetLocalDeliveryProviderAsync

        [GraphQLDescription("Gets local delivery provider")]
        public async Task<LocalDeliveryProviderResponse?> GetLocalDeliveryProviderAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetLocalDeliveryProvider(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetLocalDeliveryProvidersAsync

        [GraphQLDescription("Gets local delivery providers")]
        public async Task<PaginatedData<LocalDeliveryProviderResponse>> GetLocalDeliveryProvidersAsync(
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetLocalDeliveryProviders(paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
