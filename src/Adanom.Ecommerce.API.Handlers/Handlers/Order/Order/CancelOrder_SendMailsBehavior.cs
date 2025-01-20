using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CancelOrder_SendMailsBehavior : IPipelineBehavior<CancelOrder, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CancelOrder_SendMailsBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(CancelOrder command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var cancelOrderResponse = await next();

            if (!cancelOrderResponse)
            {
                return cancelOrderResponse;
            }

            var order = await _mediator.Send(new GetOrder(command.Id));

            if (order == null)
            {
                return cancelOrderResponse;
            }

            var user = await _mediator.Send(new GetUser(order.UserId));

            if (user == null)
            {
                return cancelOrderResponse;
            }

            var sendMailCommand = new SendMail()
            {
                Key = MailTemplateKey.ORDER_USER_CANCEL,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, order.OrderNumber }
                }
            };

            await _mediator.Publish(sendMailCommand);

            var sendToManagerMailCommand = new SendMail()
            {
                To = MailNotificationConstants.Receivers.Order,
                Key = MailTemplateKey.ADMIN_ORDER_USER_CANCEL,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, order.OrderNumber },
                }
            };

            await _mediator.Publish(sendToManagerMailCommand);

            return cancelOrderResponse;
        }

        #endregion
    }
}
