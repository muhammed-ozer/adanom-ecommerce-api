using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCartHandler : IRequestHandler<GetShoppingCart, ShoppingCartResponse?>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetShoppingCartHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ShoppingCartResponse?> Handle(GetShoppingCart command, CancellationToken cancellationToken)
        {
            var shoppingCartsQuery = _applicationDbContext.ShoppingCarts.AsNoTracking();

            ShoppingCart? shoppingCart = null;
            var updateShoppingCartItemsResponse = new UpdateShoppingCartItemsResponse();

            if (command.UserId != null && command.UserId != Guid.Empty)
            {
                shoppingCart = await shoppingCartsQuery
                    .Where(e => e.UserId == command.UserId)
                    .SingleOrDefaultAsync();

                if (shoppingCart != null && command.UpdateItems)
                {
                    updateShoppingCartItemsResponse = await _mediator.Send(new UpdateShoppingCartItems(command.UserId.Value));
                }
            }
            else if (command.Identity != null)
            {
                var userId = command.Identity.GetUserId();

                shoppingCart = await shoppingCartsQuery
                    .Where(e => e.UserId == userId)
                    .SingleOrDefaultAsync();

                if (shoppingCart != null && command.UpdateItems)
                {
                    updateShoppingCartItemsResponse = await _mediator.Send(new UpdateShoppingCartItems(command.Identity));
                }
            }
            else 
            {
                shoppingCart = await shoppingCartsQuery
                    .Where(e => e.Id == command.Id)
                    .SingleOrDefaultAsync();

                if (shoppingCart != null && command.UpdateItems)
                {
                    updateShoppingCartItemsResponse = await _mediator.Send(new UpdateShoppingCartItems(command.Id));
                }
            }

            var shoppingCartResponse = _mapper.Map<ShoppingCartResponse>(shoppingCart);

            if (shoppingCartResponse == null)
            {
                return null;
            }

            shoppingCartResponse = _mapper.Map(updateShoppingCartItemsResponse, shoppingCartResponse);

            return shoppingCartResponse;
        }

        #endregion
    }
}
