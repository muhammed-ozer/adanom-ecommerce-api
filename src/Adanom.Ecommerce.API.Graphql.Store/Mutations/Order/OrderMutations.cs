namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class OrderMutations
    {
        #region CreateOrderAsync

        [GraphQLDescription("Creates an order")]
        public async Task<OrderResponse?> CreateOrderAsync(
            CreateOrderRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateOrder(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region CancelOrderAsync

        [GraphQLDescription("Cancel an order")]
        public async Task<bool> CancelOrderAsync(
            CancelOrderRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CancelOrder(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
