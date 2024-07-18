using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateUserPermissionsHandler : IRequestHandler<UpdateUserPermissions, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;

        #endregion

        #region Ctor

        public UpdateUserPermissionsHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateUserPermissions command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var user = await _userManager.Users
                .Where(e => e.DeletedAtUtc == null && e.Id == userId)
                .SingleOrDefaultAsync();

            if (user == null)
            {
                return false;
            }

            if (command.AllowCommercialEmails)
            {
                if (!user.AllowCommercialEmails)
                {
                    user.AllowCommercialEmails = true;
                    user.AllowCommercialEmailsUpdatedAtUtc = DateTime.UtcNow;
                }
            }
            else
            {
                if (user.AllowCommercialEmails)
                {
                    user.AllowCommercialEmails = false;
                    user.AllowCommercialEmailsUpdatedAtUtc = DateTime.UtcNow;
                }
            }

            if (command.AllowCommercialSMS)
            {
                if (!user.AllowCommercialSMS)
                {
                    user.AllowCommercialSMS = true;
                    user.AllowCommercialSMSUpdatedAtUtc = DateTime.UtcNow;
                }
            }
            else
            {
                if (user.AllowCommercialSMS)
                {
                    user.AllowCommercialSMS = false;
                    user.AllowCommercialSMSUpdatedAtUtc = DateTime.UtcNow;
                }
            }

            await _userManager.UpdateAsync(user);

            return true;
        }

        #endregion
    }
}
