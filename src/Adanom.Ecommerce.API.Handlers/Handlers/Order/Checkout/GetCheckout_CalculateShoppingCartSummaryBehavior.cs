using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetCheckout_CalculateShoppingCartSummaryBehavior : IPipelineBehavior<GetCheckout, CheckoutResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetCheckout_CalculateShoppingCartSummaryBehavior(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<CheckoutResponse?> Handle(GetCheckout command, RequestHandlerDelegate<CheckoutResponse?> next, CancellationToken cancellationToken)
        {
            var checkoutResponse = await next();

            if (checkoutResponse == null)
            {
                return null;
            }

            var userId = command.Identity.GetUserId();

            var user = await _mediator.Send(new GetUser(userId));

            if (user == null)
            {
                return null;
            }

            var shoppingCart = await _mediator.Send(new GetShoppingCart(command.Identity, true, true, true, command.OrderPaymentType));

            if (shoppingCart == null)
            {
                return null;
            }

            if (!shoppingCart.Items.Any())
            {
                return null;
            }

            if (shoppingCart.Summary == null)
            {
                return null;
            }

            if (shoppingCart.HasStocksChanges || shoppingCart.HasPriceChanges || shoppingCart.HasNoItem || shoppingCart.HasProductDeleted)
            {
                checkoutResponse.CanCreateOrder = false;
            }

            checkoutResponse = _mapper.Map(shoppingCart.Summary, checkoutResponse);
            checkoutResponse = _mapper.Map(shoppingCart, checkoutResponse);

            return checkoutResponse;
        }

        #endregion
    }
}
