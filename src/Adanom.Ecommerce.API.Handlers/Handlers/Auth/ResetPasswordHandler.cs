using System.Text;
using Adanom.Ecommerce.API.Services.Mail;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class ResetPasswordHandler : IRequestHandler<ResetPassword, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public ResetPasswordHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(ResetPassword command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user == null)
            {
                return false;
            }

            var token = Encoding.UTF8.GetString(Convert.FromBase64String(command.Token));

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user!, token, command.Password);

            if (!resetPasswordResult.Succeeded)
            {
                return false;
            }

            await _mediator.Publish(new SendMail()
            {
                Key = MailTemplateKey.AUTH_PASSWORD_RESET_SUCCESSFUL,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { MailConstants.Replacements.User.FullName, $"{user.FirstName} {user.LastName}" },
                }
            });

            return true;
        }

        #endregion
    }
}
