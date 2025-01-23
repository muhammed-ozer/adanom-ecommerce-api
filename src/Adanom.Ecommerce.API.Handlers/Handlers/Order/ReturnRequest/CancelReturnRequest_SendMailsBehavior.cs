using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CancelReturnRequest_SendMailsBehavior : IPipelineBehavior<CancelReturnRequest, bool>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CancelReturnRequest_SendMailsBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(CancelReturnRequest command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var cancelReturnRequestResponse = await next();

            if (!cancelReturnRequestResponse)
            {
                return cancelReturnRequestResponse;
            }

            var returnRequest = await _mediator.Send(new GetReturnRequest(command.Id));

            if (returnRequest == null)
            {
                return cancelReturnRequestResponse;
            }

            var order = await _mediator.Send(new GetOrder(returnRequest.OrderId));

            if (order == null)
            {
                return cancelReturnRequestResponse;
            }

            var user = await _mediator.Send(new GetUser(order.UserId));

            if (user == null)
            {
                return cancelReturnRequestResponse;
            }

            var sendMailCommand = new SendMail()
            {
                Key = MailTemplateKey.RETURN_REQUEST_USER_CANCEL,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.ReturnRequest.Number, returnRequest.ReturnRequestNumber }
                }
            };

            await _mediator.Publish(sendMailCommand);

            var sendToManagerMailCommand = new SendMail()
            {
                To = MailNotificationConstants.Receivers.ReturnRequest,
                Key = MailTemplateKey.ADMIN_RETURN_REQUEST_USER_CANCEL,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.ReturnRequest.Number, returnRequest.ReturnRequestNumber },
                }
            };

            await _mediator.Publish(sendToManagerMailCommand);

            return cancelReturnRequestResponse;
        }

        #endregion
    }
}
