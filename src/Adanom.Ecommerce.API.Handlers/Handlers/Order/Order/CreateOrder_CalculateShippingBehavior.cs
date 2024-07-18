namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_CalculateShippingBehavior : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CreateOrder_CalculateShippingBehavior(
            ApplicationDbContext applicationDbContext,
            IMediator mediator,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<OrderResponse?> Handle(CreateOrder command, RequestHandlerDelegate<OrderResponse?> next, CancellationToken cancellationToken)
        {
            var orderResponse = await next();

            if (orderResponse == null)
            {
                return null;
            }

            if (orderResponse.DeliveryType.Key == DeliveryType.PICK_UP_FROM_STORE)
            {
                orderResponse.IsFreeShipping = true;
            }

            var grandTotal = orderResponse.Items.Sum(e => e.Total);

            var shippingProvider = await _mediator.Send(new GetShippingProvider(orderResponse.ShippingProviderId!.Value));

            if (shippingProvider!.MinimumFreeShippingTotalPrice <= grandTotal)
            {
                orderResponse.IsFreeShipping = true;
            }
            else
            {
                orderResponse.IsFreeShipping = false;
                orderResponse.ShippingFeeSubTotal = shippingProvider.FeeWithoutTax;
                orderResponse.ShippingFeeTax = shippingProvider.FeeTax;
            }

            return orderResponse;
        }

        #endregion
    }
}
