using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetRoleHandler : IRequestHandler<GetRole, RoleResponse?>
    {
        #region Fields

        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetRoleHandler(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<RoleResponse?> Handle(GetRole command, CancellationToken cancellationToken)
        {
            var rolesQuery = _roleManager.Roles.AsNoTracking();

            Role? role;

            if (command.Name.IsNotNullOrEmpty())
            {
                role =  await rolesQuery.SingleOrDefaultAsync(e => e.Name == command.Name);
            }
            else
            {
                role = await rolesQuery.SingleOrDefaultAsync(e => e.Id == command.Id);
            }

            return _mapper.Map<RoleResponse>(role);
        } 

        #endregion
    }
}
