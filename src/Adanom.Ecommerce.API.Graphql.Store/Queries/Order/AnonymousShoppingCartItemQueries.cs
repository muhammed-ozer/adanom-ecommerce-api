namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class AnonymousShoppingCartItemQueries
    {
        #region GetAnonymousShoppingCartItemsCountAsync

        [GraphQLDescription("Gets anonymous shopping cart items count")]
        [AllowAnonymous]
        public async Task<int> GetAnonymousShoppingCartItemsCountAsync(
            Guid anonymousSHoppingCartId,
            [Service] IMediator mediator)
        {
            var command = new GetAnonymousShoppingCartItemsCount(anonymousSHoppingCartId);

            return await mediator.Send(command);
        }

        #endregion
    }
}
