namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class BillingAddressMutations
    {
        #region CreateBillingAddressAsync

        [GraphQLDescription("Creates a billing address")]
        public async Task<bool> CreateBillingAddressAsync(
            CreateBillingAddressRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateBillingAddress(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region UpdateBillingAddressAsync

        [GraphQLDescription("Updates a billing address")]
        public async Task<bool> UpdateBillingAddressAsync(
            UpdateBillingAddressRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateBillingAddress(identity));

            return await mediator.Send(command); ;
        }

        #endregion

        #region DeleteBillingAddressAsync

        [GraphQLDescription("Deletes a billing address")]
        public async Task<bool> DeleteBillingAddressAsync(
            DeleteBillingAddressRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteBillingAddress(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
