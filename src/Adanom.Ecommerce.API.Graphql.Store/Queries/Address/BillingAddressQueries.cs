namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class BillingAddressQueries
    {
        #region GetBillingAddressAsync

        [GraphQLDescription("Gets a billing address")]
        public async Task<BillingAddressResponse?> GetBillingAddressAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetBillingAddress(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetBillingAddressesAsync

        [GraphQLDescription("Gets billing addresses")]
        public async Task<PaginatedData<BillingAddressResponse>> GetBillingAddressesAsync(
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetBillingAddresses(identity, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
