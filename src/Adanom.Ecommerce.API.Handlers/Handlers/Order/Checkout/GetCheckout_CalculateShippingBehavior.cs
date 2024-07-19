namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetCheckout_CalculateShippingBehavior : IPipelineBehavior<GetCheckout, CheckoutResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public GetCheckout_CalculateShippingBehavior(IMediator mediator)
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

            if (command.DeliveryType == DeliveryType.PICK_UP_FROM_STORE)
            {
                checkoutViewItemsResponse.IsFreeShipping = true;
            }

            var shippingProvider = await _mediator.Send(new GetShippingProvider(command.ShippingProviderId!.Value));

            if (shippingProvider!.MinimumFreeShippingTotalPrice <= checkoutViewItemsResponse.GrandTotal)
            {
                checkoutViewItemsResponse.IsFreeShipping = true;
            }
            else
            {
                checkoutViewItemsResponse.IsFreeShipping = false;
                checkoutViewItemsResponse.ShippingFeeSubTotal = shippingProvider.FeeWithoutTax;
                checkoutViewItemsResponse.ShippingFeeTax = shippingProvider.FeeTax;
            }

            checkoutViewItemsResponse.SubTotal += checkoutViewItemsResponse.ShippingFeeSubTotal + checkoutViewItemsResponse.ShippingFeeTax;
            checkoutViewItemsResponse.TaxTotal += checkoutViewItemsResponse.ShippingFeeTax;

            checkoutViewItemsResponse.GrandTotal += checkoutViewItemsResponse.ShippingFeeSubTotal + checkoutViewItemsResponse.ShippingFeeTax;

            return checkoutViewItemsResponse;
        }

        #endregion
    }
}
