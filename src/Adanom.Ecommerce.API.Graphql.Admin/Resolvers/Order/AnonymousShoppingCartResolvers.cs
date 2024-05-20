namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(AnonymousShoppingCartResponse))]
    public sealed class AnonymousShoppingCartResolvers
    {
        #region GetItemsAsync

        public async Task<IEnumerable<AnonymousShoppingCartItemResponse>> GetItemsAsync(
           [Parent] AnonymousShoppingCartResponse anonymousShoppingCartResponse,
           [Service] IMediator mediator)
        {
            var anonymousShoppingCartItems = await mediator.Send(new GetAnonymousShoppingCartItems(new GetAnonymousShoppingCartItemsFilter()
            {
                AnonymousShoppingCartId = anonymousShoppingCartResponse.Id
            }));

            return anonymousShoppingCartItems;
        }

        #endregion
    }
}
