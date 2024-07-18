using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrderHandler : IRequestHandler<CreateOrder, OrderResponse?>

    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrderHandler(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderResponse?> Handle(CreateOrder command, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateShoppingCartItems(command.Identity));

            var userId = command.Identity.GetUserId();

            var order = _mapper.Map<CreateOrder, Order>(command, options =>
            {
                options.AfterMap(async (source, target) =>
                {
                    target.UserId = userId;
                    target.OrderNumber = await _mediator.Send(new CreateOrderNumber());
                    target.OrderStatusType = OrderStatusType.PAYMENT_PENDING;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            var orderResponse = _mapper.Map<OrderResponse>(order);

            return orderResponse;
        }

        #endregion
    }
}
