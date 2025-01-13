using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetShoppingCart_GetSummaryBehavior : IPipelineBehavior<GetShoppingCart, ShoppingCartResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetShoppingCart_GetSummaryBehavior(IMediator mediator)
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

            if (!command.IncludeSummary)
            {
                return shoppingCartResponse;
            }

            if (command.Identity == null)
            {
                return shoppingCartResponse;
            }

            if (!shoppingCartResponse.Items.Any())
            {
                return null;
            }

            var userId = command.Identity.GetUserId();

            var user = await _mediator.Send(new GetUser(userId));

            if (user == null)
            {
                return null;
            }

            var shoppingCartSummaryResponse = new ShoppingCartSummaryResponse();

            foreach (var shoppingCartItem in shoppingCartResponse.Items)
            {
                var shoppingCartItemSummary = await _mediator.Send(new CalculateShoppingCartItemSummary(shoppingCartItem, user, command.OrderPaymentType));

                if (shoppingCartItemSummary == null)
                {
                    return null;
                }

                shoppingCartSummaryResponse.SubTotal += shoppingCartItemSummary.SubTotal;

                if (shoppingCartItemSummary.DiscountTotal != null && shoppingCartItemSummary.DiscountTotal > 0)
                {
                    if (shoppingCartSummaryResponse.TotalDiscount == null)
                    {
                        shoppingCartSummaryResponse.TotalDiscount = 0;
                    }

                    shoppingCartSummaryResponse.TotalDiscount += shoppingCartItemSummary.DiscountTotal.Value;
                }

                if (shoppingCartItemSummary.UserDefaultDiscountRateBasedDiscount != null && shoppingCartItemSummary.UserDefaultDiscountRateBasedDiscount > 0)
                {
                    if (shoppingCartSummaryResponse.UserDefaultDiscountRateBasedDiscount == null)
                    {
                        shoppingCartSummaryResponse.UserDefaultDiscountRateBasedDiscount = 0;
                    }

                    shoppingCartSummaryResponse.UserDefaultDiscountRateBasedDiscount += shoppingCartItemSummary.UserDefaultDiscountRateBasedDiscount;
                }

                if (shoppingCartItemSummary.DiscountByOrderPaymentType != null && shoppingCartItemSummary.DiscountByOrderPaymentType > 0)
                {
                    if (shoppingCartSummaryResponse.DiscountByOrderPaymentType == null)
                    {
                        shoppingCartSummaryResponse.DiscountByOrderPaymentType = 0;
                    }

                    shoppingCartSummaryResponse.DiscountByOrderPaymentType += shoppingCartItemSummary.DiscountByOrderPaymentType;
                }

                shoppingCartSummaryResponse.TaxTotal += shoppingCartItemSummary.TaxTotal;
            }

            shoppingCartSummaryResponse.GrandTotal = shoppingCartSummaryResponse.SubTotal - shoppingCartSummaryResponse.TotalDiscount ?? 0;

            shoppingCartResponse.Summary = shoppingCartSummaryResponse;

            return shoppingCartResponse;
        }

        #endregion
    }
}
