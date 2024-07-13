namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class PickUpStoreQueries
    {
        #region GetPickUpStoreAsync

        [AllowAnonymous]
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

        [AllowAnonymous]
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
