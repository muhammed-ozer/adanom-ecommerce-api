using System.Security.Claims;
using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateOrder_SendMailsBehavior : IPipelineBehavior<CreateOrder, OrderResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateOrder_SendMailsBehavior(IMediator mediator)
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

            var userId = command.Identity.GetUserId();
            var user = await _mediator.Send(new GetUser(userId));

            if (user == null)
            {
                return null;
            }

            // TODO: Send mail to user after order created
            //var sendToUserMailCommand = new SendMail()
            //{
            //    To = user.Email,
            //    Key = MailTemplateKey.ORDER_CREATED,
            //    Replacements = new Dictionary<string, string>()
            //    {
            //        { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
            //        { MailConstants.Replacements.Order.Number, orderResponse.OrderNumber }
            //    }
            //};

            //await _mediator.Publish(sendToUserMailCommand);

            var sendToManagerMailCommand = new SendMail()
            {
                To = MailNotificationConstants.Receivers.NewOrder,
                Key = MailTemplateKey.ADMIN_ORDER_RECEIVED,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, orderResponse.OrderNumber }
                }
            };

            await _mediator.Publish(sendToManagerMailCommand);

            return orderResponse;
        }

        #endregion
    }
}
