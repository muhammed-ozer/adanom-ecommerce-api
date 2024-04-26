using System.Security.Claims;
using MediatR;

namespace Adanom.Ecommerce.API.Commands
{
    public class ChangePassword : IRequest<bool>
    {
        public ChangePassword(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        public ClaimsPrincipal Identity { get; }

        public string OldPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;

        public string ConfirmNewPassword { get; set; } = null!;
    }
}