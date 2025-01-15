namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_StockReservationsBehavior : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrder_StockReservationsBehavior(IMapper mapper, IMediator mediator)
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

            var reservedAtUtc = DateTime.UtcNow;
            var expiresAtUtc = await _mediator.Send(new GetStockReservationExpiresAtByOrderPaymentType(command.OrderPaymentType, reservedAtUtc));

            foreach (var orderItem in orderResponse.Items)
            {
                var createStockReservationRequest = new CreateStockReservationRequest()
                {
                    OrderId = orderResponse.Id,
                    ProductId = orderItem.ProductId,
                    Amount = orderItem.Amount,
                    ReservedAtUtc = reservedAtUtc,
                    ExpiresAtUtc = expiresAtUtc
                };

                var createStockReservationCommand = _mapper.Map(createStockReservationRequest, new CreateStockReservation());

                await _mediator.Send(createStockReservationCommand);
            }
           

            return orderResponse;
        }

        #endregion
    }
}
