using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CanUserLoginHandler : IRequestHandler<CanUserLogin, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        #endregion

        #region Ctor

        public CanUserLoginHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CanUserLogin command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.Id.ToString());

            if (user == null)
            {
                return false;
            }

            var canSignIn = await _signInManager.CanSignInAsync(user);

            return canSignIn;
        } 

        #endregion
    }
}
