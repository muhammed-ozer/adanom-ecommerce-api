namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class StockNotificationItemMutations
    {
        #region CreateStockNotificationItemAsync

        [GraphQLDescription("Creates a stock notification item")]
        public async Task<bool> CreateStockNotificationItemAsync(
            CreateStockNotificationItemRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateStockNotificationItem(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
