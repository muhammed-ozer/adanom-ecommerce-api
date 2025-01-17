namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class OrderPaymentMutations
    {
        #region UpdateOrderPaymentAsync

        [GraphQLDescription("Updates an order payment")]
        public async Task<bool> UpdateOrderPaymentAsync(
            UpdateOrderPaymentRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateOrderPayment(identity));

            return await mediator.Send(command); ;
        }

        #endregion
    }
}
