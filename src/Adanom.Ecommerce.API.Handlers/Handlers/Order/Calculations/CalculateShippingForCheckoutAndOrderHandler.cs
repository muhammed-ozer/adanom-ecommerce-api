using HotChocolate.Execution;

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

            if (command.DeliveryType == DeliveryType.PICK_UP_FROM_STORE)
            {
                response.IsFreeShipping = true;
            } 
            else if(command.DeliveryType == DeliveryType.LOCAL_DELIVERY)
            {
                var localDeliveryProvider = await _mediator.Send(new GetLocalDeliveryProvider(command.LocalDeliveryProviderId!.Value));

                if (localDeliveryProvider!.MinimumOrderGrandTotal > command.GrandTotal)
                {
                    var error = new Error($"Minimum sipariş tutarı {localDeliveryProvider!.MinimumOrderGrandTotal}₺ olması gerekmektedir.", ValidationErrorCodesEnum.NOT_ALLOWED.ToString());

                    throw new QueryException(error);
                }

                if (localDeliveryProvider!.MinimumFreeDeliveryOrderGrandTotal <= command.GrandTotal)
                {
                    response.IsFreeShipping = true;
                } 
                else
                {
                    response.IsFreeShipping = false;
                    response.ShippingFeeSubTotal = localDeliveryProvider.FeeTotal;
                    response.ShippingFeeTax = _calculationService.CalculateTaxTotal(localDeliveryProvider.FeeTotal, localDeliveryProvider.TaxRate);
                }
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
