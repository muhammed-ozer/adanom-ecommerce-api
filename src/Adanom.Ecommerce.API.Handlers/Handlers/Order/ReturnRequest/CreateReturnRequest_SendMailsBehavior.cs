using System.Security.Claims;
using Adanom.Ecommerce.API.Services.Mail;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateReturnRequest_SendMailsBehavior : IPipelineBehavior<CreateReturnRequest, ReturnRequestResponse?>
    {
        #region Fields

        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateReturnRequest_SendMailsBehavior(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<ReturnRequestResponse?> Handle(CreateReturnRequest command, RequestHandlerDelegate<ReturnRequestResponse?> next, CancellationToken cancellationToken)
        {
            var returnRequestResponse = await next();

            if (returnRequestResponse == null)
            {
                return null;
            }

            var userId = command.Identity.GetUserId();
            var user = await _mediator.Send(new GetUser(userId));

            if (user == null)
            {
                return null;
            }

            var sendToUserMailCommand = new SendMail()
            {
                To = user.Email,
                Key = MailTemplateKey.RETURN_REQUEST_RECEIVED,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.ReturnRequest.Number, returnRequestResponse.ReturnRequestNumber }
                }
            };

            await _mediator.Publish(sendToUserMailCommand);

            var sendToManagerMailCommand = new SendMail()
            {
                To = MailNotificationConstants.Receivers.ReturnRequest,
                Key = MailTemplateKey.ADMIN_RETURN_REQUEST_RECEIVED,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.ReturnRequest.Number, returnRequestResponse.ReturnRequestNumber }
                }
            };

            await _mediator.Publish(sendToManagerMailCommand);

            return returnRequestResponse;
        }

        #endregion
    }
}
