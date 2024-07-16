using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteShoppingCartItem_DeleteShoppingCartBehavior : IPipelineBehavior<DeleteShoppingCartItem, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public DeleteShoppingCartItem_DeleteShoppingCartBehavior(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteShoppingCartItem command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteShoppingCartItemResponse = await next();

            if (!deleteShoppingCartItemResponse)
            {
                return deleteShoppingCartItemResponse;
            }

            var userId = command.Identity.GetUserId();

            var shoppingCart = await _applicationDbContext.ShoppingCarts
                .Where(e => e.UserId == userId)
                .Include(e => e.Items)
                .SingleOrDefaultAsync();

            if (shoppingCart == null)
            {
                return deleteShoppingCartItemResponse;
            }

            if (!shoppingCart.Items.Any())
            {
                var deleteShoppingCartResponse = await _mediator.Send(new DeleteShoppingCart(shoppingCart.Id));

                if (!deleteShoppingCartResponse)
                {
                    return false;
                }
            }

            return deleteShoppingCartItemResponse;
        }

        #endregion
    }
}
