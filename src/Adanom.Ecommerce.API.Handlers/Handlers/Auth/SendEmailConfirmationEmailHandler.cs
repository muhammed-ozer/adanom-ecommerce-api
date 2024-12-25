using System.Text;
using Adanom.Ecommerce.API.Services.Mail;
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
            var base64Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            var url = $"{UIClientConstants.Auth.BaseURL}/confirm-email?email={user.Email}&token={base64Token}";

            await _mediator.Publish(new SendMail()
            {
                Key = MailTemplateKey.AUTH_EMAIL_CONFIRMATION,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Auth.EmailConfirmationUrl, url },
                }
            });

            return true;
        } 

        #endregion
    }
}
