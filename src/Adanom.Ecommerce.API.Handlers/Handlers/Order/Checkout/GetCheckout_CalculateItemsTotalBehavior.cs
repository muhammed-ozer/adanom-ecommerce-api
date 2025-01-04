using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetCheckout_CalculateItemsTotalBehavior : IPipelineBehavior<GetCheckout, CheckoutResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetCheckout_CalculateItemsTotalBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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

            var shoppingCart = await _mediator.Send(new GetShoppingCart(command.Identity, false));

            if (shoppingCart == null)
            {
                return null;
            }

            var shoppingCartItems = await _mediator.Send(new GetShoppingCartItems(new GetShoppingCartItemsFilter()
            {
                ShoppingCartId = shoppingCart.Id
            }));

            if (!shoppingCartItems.Any())
            {
                return null;
            }

            shoppingCart.Items = shoppingCartItems.ToList();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var calculatedItemResponse = await _mediator.Send(new CalculateShoppingCartItemTotalsForCheckoutAndOrder(shoppingCartItem, user));

                if (calculatedItemResponse == null)
                {
                    return null;
                }

                checkoutResponse.SubTotal += calculatedItemResponse.SubTotal;

                if (calculatedItemResponse.SubDiscountedTotal != null && calculatedItemResponse.SubDiscountedTotal > 0)
                {
                    checkoutResponse.SubTotalDiscount += calculatedItemResponse.SubTotal - calculatedItemResponse.SubDiscountedTotal.Value;
                }

                if (calculatedItemResponse.UserDefaultDiscountRateBasedDiscount != null && calculatedItemResponse.UserDefaultDiscountRateBasedDiscount > 0)
                {
                    if (checkoutResponse.UserDefaultDiscountRateBasedDiscount == null)
                    {
                        checkoutResponse.UserDefaultDiscountRateBasedDiscount = calculatedItemResponse.UserDefaultDiscountRateBasedDiscount;
                    }
                    else
                    {
                        checkoutResponse.UserDefaultDiscountRateBasedDiscount += calculatedItemResponse.UserDefaultDiscountRateBasedDiscount;
                    }
                }

                checkoutResponse.TaxTotal += calculatedItemResponse.TaxTotal;
            }

            checkoutResponse.GrandTotal = checkoutResponse.SubTotal - checkoutResponse.SubTotalDiscount + checkoutResponse.TaxTotal;

            checkoutResponse.ShoppingCart = shoppingCart;

            return checkoutResponse;
        }

        #endregion
    }
}
