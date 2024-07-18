namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize]
    public class CheckoutQueries
    {
        #region GetCheckoutAsync

        [GraphQLDescription("Gets checkout")]
        public async Task<CheckoutResponse?> GetCheckoutAsync(
            CheckoutRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new GetCheckout(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
