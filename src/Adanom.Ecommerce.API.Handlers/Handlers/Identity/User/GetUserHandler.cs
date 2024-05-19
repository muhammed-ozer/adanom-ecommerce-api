using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetUserHandler : IRequestHandler<GetUser, UserResponse?>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetUserHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<UserResponse?> Handle(GetUser command, CancellationToken cancellationToken)
        {
            var usersQuery = _userManager.Users
                .Where(e => e.DeletedAtUtc == null)
                .AsNoTracking();

            User? user;

            if (command.Email.IsNotNullOrEmpty())
            {
                user =  await usersQuery.SingleOrDefaultAsync(e => e.Email == command.Email);
            }
            else
            {
                user = await usersQuery.SingleOrDefaultAsync(e => e.Id == command.Id);
            }

            if (user == null)
            {
                return null;
            }

            var userResponse = _mapper.Map<UserResponse>(user);

            if (userResponse != null)
            {
                if (command.IncludeRoles != null && command.IncludeRoles.Value)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    userResponse.Roles = [..roles];
                }
            }

            return userResponse;
        } 

        #endregion
    }
}
