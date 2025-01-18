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

            if (command.OrderPaymentType == OrderPaymentType.ONLINE_PAYMENT)
            {
                return orderResponse;
            }

            var user = await _mediator.Send(new GetUser(orderResponse.UserId));

            if (user == null)
            {
                return orderResponse;
            }

            var sendMailCommand = new SendMail()
            {
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, orderResponse.OrderNumber }
                }
            };

            //TODO: Send documents when order status type is not online payment, because we pending payment when order payment type is online payment
            // TODO: Attach documents

            if (command.OrderPaymentType == OrderPaymentType.BANK_TRANSFER)
            {
                sendMailCommand.Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_NEW_ORDERPAYMENTTYPE_BANK_TRANSFER;

                sendMailCommand.Replacements.Add(
                            new KeyValuePair<string, string>(MailConstants.Replacements.Order.GrandTotal, $"{orderResponse.GrandTotal}₺"));
            }
            else
            {
                sendMailCommand.Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_NEW;
            }

            await _mediator.Publish(sendMailCommand);

            var deliveryType = await _mediator.Send(new GetDeliveryType(orderResponse.DeliveryType.Key));
            var orderPaymentType = await _mediator.Send(new GetOrderPaymentType(command.OrderPaymentType));

            var sendToManagerMailCommand = new SendMail()
            {
                To = MailNotificationConstants.Receivers.NewOrder,
                Key = MailTemplateKey.ADMIN_ORDER_RECEIVED,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, orderResponse.OrderNumber },
                    { MailConstants.Replacements.Order.DeliveryType, deliveryType.Name },
                    { MailConstants.Replacements.Order.OrderPaymentType, orderPaymentType.Name },
                }
            };

            await _mediator.Publish(sendToManagerMailCommand);

            return orderResponse;
        }

        #endregion
    }
}
