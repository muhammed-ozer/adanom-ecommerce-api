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
            var checkoutViewItemsResponse = await next();

            if (checkoutViewItemsResponse == null)
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
                var productPrice = await _mediator.Send(new GetProductPriceByProductId(shoppingCartItem.ProductId));

                if (productPrice == null)
                {
                    return null;
                }

                var taxCategory = await _mediator.Send(new GetTaxCategory(productPrice.TaxCategoryId));

                if (taxCategory == null)
                {
                    return null;
                }

                var subTotal =  shoppingCartItem.OriginalPrice * shoppingCartItem.Amount;
                decimal? subDiscountedTotal = null;

                if (shoppingCartItem.DiscountedPrice != null && shoppingCartItem.DiscountedPrice.Value != 0)
                {
                    subDiscountedTotal = shoppingCartItem.DiscountedPrice.Value * shoppingCartItem.Amount;
                }
                else if (user.DefaultDiscountRate > 0)
                {
                    var discountAmount = Calculations.CalculateDiscountedPriceByDiscountRate(shoppingCartItem.OriginalPrice, user.DefaultDiscountRate);
                    var discountedPrice = shoppingCartItem.OriginalPrice - discountAmount;

                    subDiscountedTotal = discountedPrice * shoppingCartItem.Amount;
                }

                decimal taxTotal;

                if (subDiscountedTotal != null && subDiscountedTotal > 0)
                {
                    taxTotal = Calculations.CalculateTaxFromIncludedTaxTotal(subDiscountedTotal.Value, taxCategory.Rate / 100m);
                }
                else
                {
                    taxTotal = Calculations.CalculateTaxFromIncludedTaxTotal(subTotal, taxCategory.Rate / 100m);
                }

                checkoutViewItemsResponse.SubTotal += subTotal;

                if (subDiscountedTotal != null && subDiscountedTotal > 0)
                {
                    checkoutViewItemsResponse.SubTotalDiscount += subTotal - subDiscountedTotal.Value;
                }

                checkoutViewItemsResponse.TaxTotal += taxTotal;
            }

            checkoutViewItemsResponse.GrandTotal = checkoutViewItemsResponse.SubTotal - checkoutViewItemsResponse.SubTotalDiscount;

            checkoutViewItemsResponse.ShoppingCart = shoppingCart;

            return checkoutViewItemsResponse;
        }

        #endregion
    }
}
