namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class ShoppingCartMutations
    {
        #region MigrateAnonymousShoppingCartToShoppingCartAsync

        [GraphQLDescription("Migrates anonymous shopping cart to shopping cart")]
        public async Task<bool> MigrateAnonymousShoppingCartToShoppingCartAsync(
            MigrateAnonymousShoppingCartToShoppingCartRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new MigrateAnonymousShoppingCartToShoppingCart(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
