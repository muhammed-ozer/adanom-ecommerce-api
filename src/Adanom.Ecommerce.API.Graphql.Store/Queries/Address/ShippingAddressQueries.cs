namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class ShippingAddressQueries
    {
        #region GetShippingAddressAsync

        [GraphQLDescription("Gets a shipping address")]
        public async Task<ShippingAddressResponse?> GetShippingAddressAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetShippingAddress(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetShippingAddressesAsync

        [GraphQLDescription("Gets shipping addresses")]
        public async Task<PaginatedData<ShippingAddressResponse>> GetShippingAddressesAsync(
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new GetShippingAddresses(identity, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
