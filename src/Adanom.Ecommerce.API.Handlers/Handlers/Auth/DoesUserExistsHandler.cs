using Adanom.Ecommerce.API.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesUserExistsHandler : IRequestHandler<DoesUserExists, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;

        #endregion

        #region Ctor

        public DoesUserExistsHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesUserExists command, CancellationToken cancellationToken)
        {
            if (command.Id is not null)
            {
                var userExists = await _userManager.Users
                    .Where(e => 
                        e.DeletedAtUtc == null && 
                        e.Id == command.Id)
                    .AnyAsync();

                if (!userExists)
                {
                    return false;
                }
            }
            else if (command.Email is not null)
            {
                var userExists = await _userManager.Users
                    .Where(e =>
                        e.DeletedAtUtc == null &&
                        e.Email == command.Email)
                    .AnyAsync();

                if (!userExists)
                {
                    return false;
                }
            }

            return true;
        } 

        #endregion
    }
}
