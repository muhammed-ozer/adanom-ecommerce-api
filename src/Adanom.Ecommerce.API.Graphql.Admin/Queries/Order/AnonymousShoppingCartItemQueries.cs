namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class AnonymousShoppingCartItemQueries
    {
        #region GetAnonymousShoppingCartItemsAsync

        [GraphQLDescription("Gets anonymous shopping cart items")]
        public async Task<IEnumerable<AnonymousShoppingCartItemResponse>> GetAnonymousShoppingCartItemsAsync(
            GetAnonymousShoppingCartItemsFilter filter,
            [Service] IMediator mediator)
        {
            var command = new GetAnonymousShoppingCartItems(filter);

            return await mediator.Send(command);
        }

        #endregion
    }
}
