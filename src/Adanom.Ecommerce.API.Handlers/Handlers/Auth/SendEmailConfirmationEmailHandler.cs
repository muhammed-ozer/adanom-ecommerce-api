using System.Web;
using Adanom.Ecommerce.API.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class SendEmailConfirmationEmailHandler : IRequestHandler<SendEmailConfirmationEmail, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public SendEmailConfirmationEmailHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(SendEmailConfirmationEmail command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user == null)
            {
                return false;
            }

            if (user.EmailConfirmed)
            {
                return true;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var url = $"{Store.Url}/email-confirmation?email={user.Email}&token={HttpUtility.UrlEncode(token)}";

            await _mediator.Publish(new SendMail()
            {
                Key = MailTemplateKey.EMAIL_CONFIRMATION,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { "{USER_NAME}", $"{user.FirstName} {user.LastName}" },
                    { "{EMAIL_CONFIRMATION_LINK}", url }
                }
            });

            // TODO: Update mail template

            return true;
        } 

        #endregion
    }
}
