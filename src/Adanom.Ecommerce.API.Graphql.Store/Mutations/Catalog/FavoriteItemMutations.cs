namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class FavoriteItemMutations
    {
        #region CreateFavoriteItemAsync

        [GraphQLDescription("Creates a favorite item")]
        public async Task<bool> CreateFavoriteItemAsync(
            CreateFavoriteItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateFavoriteItem(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region DeleteFavoriteItemAsync

        [GraphQLDescription("Deletes a favorite item")]
        public async Task<bool> DeleteFavoriteItemAsync(
            DeleteFavoriteItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new DeleteFavoriteItem(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
