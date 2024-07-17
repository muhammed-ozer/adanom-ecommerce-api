namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ShoppingCartItemResponse))]
    public sealed class ShoppingCartItemResolvers
    {
        #region GetShoppingCartAsync

        public async Task<ShoppingCartResponse?> GetShoppingCartAsync(
           [Parent] ShoppingCartItemResponse shoppingCartItemResponse,
           [Service] IMediator mediator)
        {
            var shoppingCart = await mediator.Send(new GetShoppingCart(shoppingCartItemResponse.ShoppingCartId, true));

            return shoppingCart;
        }

        #endregion

        #region GetProductAsync

        public async Task<ProductResponse?> GetProductAsync(
           [Parent] ShoppingCartItemResponse shoppingCartItemResponse,
           [Service] IMediator mediator)
        {
            var product = await mediator.Send(new GetProduct(shoppingCartItemResponse.ProductId));

            return product;
        }

        #endregion
    }
}
