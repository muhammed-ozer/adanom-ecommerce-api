using System.Text;
using Adanom.Ecommerce.API.Services.Mail;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class SendPasswordResetEmailHandler : IRequestHandler<SendPasswordResetEmail, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public SendPasswordResetEmailHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(SendPasswordResetEmail command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var base64Token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            var url = $"{UIClientConstants.Auth.BaseURL}/reset-password?email={user!.Email}&token={base64Token}";

            await _mediator.Publish(new SendMail()
            {
                Key = MailTemplateKey.AUTH_PASSWORD_RESET,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                    { MailConstants.Replacements.Auth.PasswordResetUrl, url }
                }
            });

            return true;
        }

        #endregion
    }
}
