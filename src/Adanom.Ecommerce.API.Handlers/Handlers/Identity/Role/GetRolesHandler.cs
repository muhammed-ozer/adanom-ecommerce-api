using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetRolesHandler : IRequestHandler<GetRoles, IEnumerable<RoleResponse>>

    {
        #region Fields

        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetRolesHandler(
            RoleManager<Role> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<RoleResponse>> Handle(GetRoles command, CancellationToken cancellationToken)
        {
            var rolesQuery = _roleManager.Roles
                .AsNoTracking();

            var roleResponses = _mapper.Map<IEnumerable<RoleResponse>>(await rolesQuery.ToListAsync());

            return roleResponses;
        }

        #endregion
    }
}
