using System.Web;
using Adanom.Ecommerce.API.Data.Models;
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

            var token = await _userManager.GeneratePasswordResetTokenAsync(user!);

            var url = $"{Store.Url}/reset-password?email={user!.Email}&token={HttpUtility.UrlEncode(token)}";

            await _mediator.Publish(new SendMail()
            {
                Key = MailTemplateKey.PASSWORD_RESET,
                To = user.Email,
                Replacements = new Dictionary<string, string>()
                {
                    { "{USER_NAME}", $"{user.FirstName} {user.LastName}" },
                    { "{PASSWORD_RESET_LINK}", url }
                }
            });

            // TODO: Update mail template

            return true;
        } 

        #endregion
    }
}
