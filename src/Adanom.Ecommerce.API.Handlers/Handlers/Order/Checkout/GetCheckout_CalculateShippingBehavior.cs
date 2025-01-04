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
            var checkoutResponse = await next();

            if (checkoutResponse == null)
            {
                return null;
            }

            var calculatedShippingResponse = await _mediator.Send(new CalculateShippingForCheckoutAndOrder(
                command.DeliveryType,
                checkoutResponse.GrandTotal,
                command.ShippingProviderId));

            checkoutResponse.IsFreeShipping = calculatedShippingResponse!.IsFreeShipping;
            checkoutResponse.ShippingFeeSubTotal = calculatedShippingResponse.ShippingFeeSubTotal;
            checkoutResponse.ShippingFeeTax = calculatedShippingResponse.ShippingFeeTax;

            checkoutResponse.GrandTotal += checkoutResponse.ShippingFeeSubTotal + checkoutResponse.ShippingFeeTax;

            return checkoutResponse;
        }

        #endregion
    }
}
