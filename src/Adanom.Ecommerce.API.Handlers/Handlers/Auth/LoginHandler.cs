using Adanom.Ecommerce.API.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class LoginHandler : IRequestHandler<Login, UserResponse?>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public LoginHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMediator mediator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(userManager));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<UserResponse?> Handle(Login command, CancellationToken cancellationToken)
        {
            var isValid = await ValidateAsync(command);

            if (!isValid)
            {
                return null;
            }

            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user == null)
            {
                return null;
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);

            if (!signInResult.Succeeded)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new UserResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToList()
            };
        }

        #endregion

        #region ValidateAsync

        private async Task<bool> ValidateAsync(Login command)
        {
            if (string.IsNullOrEmpty(command.Email) || string.IsNullOrEmpty(command.Password))
            {
                return false;
            }

            var userExists = await _mediator.Send(new DoesUserExists(command.Email));

            if (!userExists)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
