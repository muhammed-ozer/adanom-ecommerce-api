using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateOrder_OrderStatusTypeCreateNotification : IPipelineBehavior<UpdateOrder_OrderStatusType, bool>
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateOrder_OrderStatusTypeCreateNotification(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(UpdateOrder_OrderStatusType command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var updateOrder_OrderStatusTypeResponse = await next();

            if (!updateOrder_OrderStatusTypeResponse)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var order = await _mediator.Send(new GetOrder(command.Id));

            if (order == null)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var orderStatusType = order.OrderStatusType.Key;

            if (command.OldOrderStatusType == OrderStatusType.PAYMENT_PENDING && orderStatusType == OrderStatusType.NEW)
            {
                await _mediator.Publish(new CreateNotification(
                    NotificationType.NEW_ORDER,
                     string.Format(NotificationConstants.NewOrder, order.OrderNumber)));
            }

            return updateOrder_OrderStatusTypeResponse;
        }

        #endregion
    }
}
