namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class PickUpStoreQueries
    {
        #region GetPickUpStoreAsync

        [GraphQLDescription("Gets pick up store")]
        public async Task<PickUpStoreResponse?> GetPickUpStoreAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetPickUpStore(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetPickUpStoresAsync

        [GraphQLDescription("Gets pick up stores")]
        public async Task<PaginatedData<PickUpStoreResponse>> GetPickUpStoresAsync(
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetPickUpStores(paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
