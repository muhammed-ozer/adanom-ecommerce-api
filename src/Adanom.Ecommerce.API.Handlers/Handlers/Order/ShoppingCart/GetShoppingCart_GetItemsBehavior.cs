namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCart_GetItemsBehavior : IPipelineBehavior<GetShoppingCart, ShoppingCartResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetShoppingCart_GetItemsBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ShoppingCartResponse?> Handle(GetShoppingCart command, RequestHandlerDelegate<ShoppingCartResponse?> next, CancellationToken cancellationToken)
        {
            var shoppingCartResponse = await next();

            if (shoppingCartResponse == null)
            {
                return null;
            }

            if (!command.IncludeItems)
            {
                return shoppingCartResponse;
            }

            var shoppingCartItems = await _mediator.Send(new GetShoppingCartItems(new GetShoppingCartItemsFilter()
            {
                ShoppingCartId = shoppingCartResponse.Id
            }));

            shoppingCartResponse.Items = shoppingCartItems.ToList();

            return shoppingCartResponse;
        }

        #endregion
    }
}
