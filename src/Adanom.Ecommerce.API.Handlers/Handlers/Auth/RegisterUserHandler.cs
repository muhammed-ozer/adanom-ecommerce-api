using Adanom.Ecommerce.API.Data.Models;
using Adanom.Ecommerce.API.Security;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class RegisterUserHandler : IRequestHandler<RegisterUser, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public RegisterUserHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(RegisterUser command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<RegisterUser, User>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UserName = command.Email;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            if (command.AllowCommercialEmails)
            {
                user.AllowCommercialEmailsUpdatedAtUtc = DateTime.UtcNow;
            }

            if (command.AllowCommercialSMS)
            {
                user.AllowCommercialSMSUpdatedAtUtc = DateTime.UtcNow;
            }

            var createAccountResult = await _userManager.CreateAsync(user, command.Password);

            if (!createAccountResult.Succeeded)
            {
                return false;
            }

            await _userManager.AddToRoleAsync(user, SecurityConstants.Roles.Customer);

            return true;
        } 

        #endregion
    }
}
