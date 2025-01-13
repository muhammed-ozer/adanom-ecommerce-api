using HotChocolate.Execution;

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

            var grandTotal = orderResponse.Items.Sum(e => e.Total);

            var calculatedShippingResponse = await _mediator.Send(new CalculateShippingForCheckoutAndOrder(
                orderResponse.DeliveryType.Key,
                grandTotal,
                command.ShippingProviderId,
                command.LocalDeliveryProviderId));

            if (calculatedShippingResponse == null)
            {
                return null;
            }

            orderResponse.IsFreeShipping = calculatedShippingResponse.IsFreeShipping;
            orderResponse.ShippingFeeTotal = calculatedShippingResponse.ShippingFeeTotal;
            orderResponse.ShippingFeeTax = calculatedShippingResponse.ShippingFeeTax;

            if (!calculatedShippingResponse.IsShippable)
            {
                var error = new Error(calculatedShippingResponse.ErrorMessage ?? "Teslimat yöntemi şuan kullanılamaz.", ValidationErrorCodesEnum.NOT_ALLOWED.ToString());
                throw new QueryException(error);
            }

            return orderResponse;
        }

        #endregion
    }
}
