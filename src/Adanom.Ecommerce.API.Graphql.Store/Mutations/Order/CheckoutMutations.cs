namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class CheckoutMutations
    {
        #region ProceedToCheckoutAsync

        [GraphQLDescription("Proceeds to checkout")]
        public async Task<bool> ProceedToCheckoutAsync(
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = new ProceedToCheckout(identity);

            return await mediator.Send(command);
        }

        #endregion
    }
}
