namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_CreateOrderPaymentBehavior : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrder_CreateOrderPaymentBehavior(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

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

            var createOrderPaymentRequest = new CreateOrderPaymentRequest()
            {
                OrderId = orderResponse.Id,
                OrderPaymentType = command.OrderPaymentType
            };

            if (command.OrderPaymentType == OrderPaymentType.ONLINE_PAYMENT)
            {
                //TODO: Implement payment gateway
            }

            var createOrderPaymentCommand = _mapper.Map(createOrderPaymentRequest, new CreateOrderPayment(command.Identity));

            await _mediator.Send(createOrderPaymentCommand);

            return orderResponse;
        }

        #endregion
    }
}
