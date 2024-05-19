using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesRoleExistsHandler : IRequestHandler<DoesRoleExists, bool>
    {
        #region Fields

        private readonly RoleManager<Role> _roleManager;

        #endregion

        #region Ctor

        public DoesRoleExistsHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesRoleExists command, CancellationToken cancellationToken)
        {
            if (command.Name is not null)
            {
                return await _roleManager.Roles
                    .Where(e => e.Name == command.Name)
                    .AnyAsync();
            }
            else 
            {
                return await _roleManager.Roles
                    .Where(e => e.Id == command.Id)
                    .AnyAsync();
            }
        } 

        #endregion
    }
}
