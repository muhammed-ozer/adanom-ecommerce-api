namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class ShoppingCartItemMutations
    {
        #region CreateShoppingCartItemAsync

        [GraphQLDescription("Creates a shopping cart item")]
        public async Task<bool> CreateShoppingCartItemAsync(
            CreateShoppingCartItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateShoppingCartItem(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region UpdateShoppingCartItemAsync

        [GraphQLDescription("Updates a shopping cart item")]
        public async Task<bool> UpdateShoppingCartItemAsync(
            UpdateShoppingCartItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateShoppingCartItem(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region DeleteShoppingCartItemAsync

        [GraphQLDescription("Deletes a shopping cart item")]
        public async Task<bool> DeleteShoppingCartItemAsync(
            DeleteShoppingCartItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteShoppingCartItem(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
