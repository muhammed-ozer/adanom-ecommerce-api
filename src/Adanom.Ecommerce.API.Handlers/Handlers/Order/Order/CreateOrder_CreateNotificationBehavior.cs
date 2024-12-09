namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_CreateNotificationBehavior : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrder_CreateNotificationBehavior(IMediator mediator)
        {
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

            await _mediator.Publish(new CreateNotification(
                NotificationType.NEW_ORDER,
                 string.Format(NotificationConstants.NewOrder, orderResponse.OrderNumber)));

            return orderResponse;
        }

        #endregion
    }
}
