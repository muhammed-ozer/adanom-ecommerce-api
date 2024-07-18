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

                var totalPrice = shoppingCartItem.Price * shoppingCartItem.Amount;
                var taxRate = taxCategory.Rate / 100m;

                var subTotal = decimal.Round(totalPrice / (1 + taxRate), 2, MidpointRounding.AwayFromZero);
                var taxTotal = decimal.Round(totalPrice - subTotal, 2, MidpointRounding.AwayFromZero);

                checkoutViewItemsResponse.SubTotal += subTotal;
                checkoutViewItemsResponse.TaxTotal += taxTotal;
            }

            checkoutViewItemsResponse.GrandTotal = checkoutViewItemsResponse.SubTotal + checkoutViewItemsResponse.TaxTotal;

            checkoutViewItemsResponse.ShoppingCart = shoppingCart;

            return checkoutViewItemsResponse;
        }

        #endregion
    }
}
