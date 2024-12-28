namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public sealed class AnonymousShoppingCartItemMutations
    {
        #region CreateAnonymousShoppingCartItemAsync

        [GraphQLDescription("Creates an anonymous shopping cart item")]
        [AllowAnonymous]
        public async Task<CreateAnonymousShoppingCartItemResponse> CreateAnonymousShoppingCartItemAsync(
            CreateAnonymousShoppingCartItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper)
        {
            var command = mapper.Map(request, new CreateAnonymousShoppingCartItem());

            return await mediator.Send(command);
        }

        #endregion
    }
}
