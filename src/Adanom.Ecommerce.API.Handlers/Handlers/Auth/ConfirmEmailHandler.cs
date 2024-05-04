using System.Web;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class ConfirmEmailHandler : IRequestHandler<ConfirmEmail, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public ConfirmEmailHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(ConfirmEmail command, CancellationToken cancellationToken)
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

            var token = HttpUtility.UrlDecode(command.Token);

            var confirmationResult = await _userManager.ConfirmEmailAsync(user, token);

            if (!confirmationResult.Succeeded)
            {
                return false;
            }

            await _mediator.Publish(new SendMail()
            {
                Key = MailTemplateKey.EMAIL_CONFIRMED_SUCCESSFUL,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { "{USER_NAME}", $"{user.FirstName} {user.LastName}" }
                }
            });

            // TODO: Update mail template

            return true;
        } 

        #endregion
    }
}
