using System.Security.Claims;
using Adanom.Ecommerce.API.Logging;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class ChangePasswordHandler : IRequestHandler<ChangePassword, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public ChangePasswordHandler(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(ChangePassword command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                await _mediator.Publish(new CreateLog(new AuthLogRequest()
                {
                    LogLevel = LogLevel.INFORMATION,
                    Description = string.Format(LogMessages.Auth.UserNotFound, userId, "-")
                }));

                return false;
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user!, command.OldPassword, command.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                return false;
            }

            await _mediator.Publish(new SendMail()
            {
                Key = MailTemplateKey.PASSWORD_CHANGED_SUCCESSFUL,
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
