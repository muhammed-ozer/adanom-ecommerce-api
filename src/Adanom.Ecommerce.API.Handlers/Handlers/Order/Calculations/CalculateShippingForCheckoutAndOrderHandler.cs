namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CalculateShippingForCheckoutAndOrderHandler
        : IRequestHandler<CalculateShippingForCheckoutAndOrder, CalculateShippingForCheckoutAndOrderResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly ICalculationService _calculationService;

        #endregion

        #region Ctor

        public CalculateShippingForCheckoutAndOrderHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            ICalculationService calculationService)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<CalculateShippingForCheckoutAndOrderResponse?> Handle(CalculateShippingForCheckoutAndOrder command, CancellationToken cancellationToken)
        {
            var response = new CalculateShippingForCheckoutAndOrderResponse();

            response.IsShippable = true;

            if (command.DeliveryType == DeliveryType.PICK_UP_FROM_STORE)
            {
                response.IsFreeShipping = true;
            } 
            else if(command.DeliveryType == DeliveryType.LOCAL_DELIVERY)
            {
                var localDeliveryProvider = await _mediator.Send(new GetLocalDeliveryProvider(command.LocalDeliveryProviderId!.Value));

                if (localDeliveryProvider!.MinimumOrderGrandTotal > command.GrandTotal)
                {
                    response.IsShippable = false;
                    response.ErrorMessage = $"Minimum sepet tutarı {localDeliveryProvider!.MinimumOrderGrandTotal}₺ olması gerekmektedir.";

                    return response;
                }

                if (localDeliveryProvider!.MinimumFreeDeliveryOrderGrandTotal <= command.GrandTotal)
                {
                    response.IsFreeShipping = true;
                } 
                else
                {
                    response.IsFreeShipping = false;
                    response.ShippingFeeTotal = localDeliveryProvider.FeeTotal;
                    response.ShippingFeeTax = _calculationService.CalculateTaxTotal(localDeliveryProvider.FeeTotal, localDeliveryProvider.TaxRate);
                }
            }
            else
            {
                var shippingProvider = await _mediator.Send(new GetShippingProvider(command.ShippingProviderId!.Value));

                if (shippingProvider!.MinimumOrderGrandTotal > command.GrandTotal)
                {
                    response.IsShippable = false;
                    response.ErrorMessage = $"Minimum sepet tutarı {shippingProvider!.MinimumOrderGrandTotal}₺ olması gerekmektedir.";

                    return response;
                }

                if (shippingProvider!.MinimumFreeShippingOrderGrandTotal <= command.GrandTotal)
                {
                    response.IsFreeShipping = true;
                }
                else
                {
                    response.IsFreeShipping = false;
                    response.ShippingFeeTotal = shippingProvider.FeeTotal;
                    response.ShippingFeeTax = _calculationService.CalculateTaxTotal(shippingProvider.FeeTotal, shippingProvider.TaxRate);
                }
            }

            return response;
        }

        #endregion
    }
}
