using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateOrder_OrderStatusTypeNewBehavior : IPipelineBehavior<UpdateOrder_OrderStatusType, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateOrder_OrderStatusTypeNewBehavior(IMediator mediator)
        {
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

            if (command.OldOrderStatusType == command.NewOrderStatusType)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            if (command.NewOrderStatusType != OrderStatusType.NEW)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var order = await _mediator.Send(new GetOrder(command.Id));

            if (order == null)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var user = await _mediator.Send(new GetUser(order.UserId));

            if (user == null)
            {
                return updateOrder_OrderStatusTypeResponse;
            }

            var sendMailCommand = new SendMail()
            {
                Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_NEW,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, order.OrderNumber }
                }
            };

            // TODO: Send documents when order payment successful
            // TODO: Attach documents

            await _mediator.Publish(sendMailCommand);

            // Delete stock reservations when online payment successful
            if (command.OrderPaymentType == OrderPaymentType.ONLINE_PAYMENT)
            {
                await _mediator.Send(new DeleteStockReservations(order.Id, true));
            }

            var sendToManagerMailCommand = new SendMail()
            {
                To = MailNotificationConstants.Receivers.NewOrder,
                Key = MailTemplateKey.ADMIN_ORDER_RECEIVED,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, order.OrderNumber }
                }
            };

            await _mediator.Publish(sendToManagerMailCommand);

            return updateOrder_OrderStatusTypeResponse;
        }

        #endregion
    }
}
