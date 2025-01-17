using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateOrder_OrderStatusTypeCancelBehavior : IPipelineBehavior<UpdateOrder_OrderStatusType, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateOrder_OrderStatusTypeCancelBehavior(IMediator mediator)
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

            if (command.NewOrderStatusType != OrderStatusType.CANCEL)
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

            await _mediator.Send(new DeleteStockReservations(order.Id, false));

            var sendMailCommand = new SendMail()
            {
                Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_CANCEL,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Order.Number, order.OrderNumber }
                }
            };

            await _mediator.Publish(sendMailCommand);

            return updateOrder_OrderStatusTypeResponse;
        }

        #endregion
    }
}
