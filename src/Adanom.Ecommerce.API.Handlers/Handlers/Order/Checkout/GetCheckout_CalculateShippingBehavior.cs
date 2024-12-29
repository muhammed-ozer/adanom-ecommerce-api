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

            var calculatedShippingResponse = await _mediator.Send(new CalculateShippingForCheckoutAndOrder(
                command.DeliveryType,
                checkoutViewItemsResponse.GrandTotal,
                command.ShippingProviderId));

            checkoutViewItemsResponse.IsFreeShipping = calculatedShippingResponse!.IsFreeShipping;
            checkoutViewItemsResponse.ShippingFeeSubTotal = calculatedShippingResponse.ShippingFeeSubTotal;
            checkoutViewItemsResponse.ShippingFeeTax = calculatedShippingResponse.ShippingFeeTax;

            checkoutViewItemsResponse.SubTotal += checkoutViewItemsResponse.ShippingFeeSubTotal + checkoutViewItemsResponse.ShippingFeeTax;
            checkoutViewItemsResponse.TaxTotal += checkoutViewItemsResponse.ShippingFeeTax;

            checkoutViewItemsResponse.GrandTotal += checkoutViewItemsResponse.ShippingFeeSubTotal + checkoutViewItemsResponse.ShippingFeeTax;

            return checkoutViewItemsResponse;
        }

        #endregion
    }
}
