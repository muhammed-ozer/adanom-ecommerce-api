using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateUserRolesHandler : IRequestHandler<UpdateUserRoles, bool>
    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateUserRolesHandler(
            UserManager<User> userManager,
            IMediator mediator)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateUserRoles command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var user = await _userManager.Users
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleAsync();

            var userRoles = await _userManager.GetRolesAsync(user);

            var addUserRoles = command.Roles.Except(userRoles).ToList();
            var removeUserRoles = userRoles.Except(command.Roles).ToList();

            foreach (var role in addUserRoles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            foreach (var role in removeUserRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.USER,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, user.Id),
            }));

            return true;
        }

        #endregion
    }
}
