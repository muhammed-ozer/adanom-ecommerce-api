namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CalculateShippingForCheckoutAndOrderHandler
        : IRequestHandler<CalculateShippingForCheckoutAndOrder, CalculateShippingForCheckoutAndOrderResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CalculateShippingForCheckoutAndOrderHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<CalculateShippingForCheckoutAndOrderResponse?> Handle(CalculateShippingForCheckoutAndOrder command, CancellationToken cancellationToken)
        {
            var response = new CalculateShippingForCheckoutAndOrderResponse();

            if (command.DeliveryType == DeliveryType.PICK_UP_FROM_STORE)
            {
                response.IsFreeShipping = true;
            }
            else
            {
                var shippingProvider = await _mediator.Send(new GetShippingProvider(command.ShippingProviderId!.Value));

                if (shippingProvider!.MinimumFreeShippingTotalPrice <= command.GrandTotal)
                {
                    response.IsFreeShipping = true;
                }
                else
                {
                    response.IsFreeShipping = false;
                    response.ShippingFeeSubTotal = shippingProvider.FeeWithoutTax;
                    response.ShippingFeeTax = shippingProvider.FeeTax;
                }
            }

            return response;
        }

        #endregion
    }
}
