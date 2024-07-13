namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class ShippingAddressMutations
    {
        #region CreateShippingAddressAsync

        [GraphQLDescription("Creates a shipping address")]
        public async Task<bool> CreateShippingAddressAsync(
            CreateShippingAddressRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateShippingAddress(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateShippingAddressAsync

        [GraphQLDescription("Updates a shipping address")]
        public async Task<bool> UpdateShippingAddressAsync(
            UpdateShippingAddressRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateShippingAddress(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteShippingAddressAsync

        [GraphQLDescription("Deletes a shipping address")]
        public async Task<bool> DeleteShippingAddressAsync(
            DeleteShippingAddressRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteShippingAddress(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
