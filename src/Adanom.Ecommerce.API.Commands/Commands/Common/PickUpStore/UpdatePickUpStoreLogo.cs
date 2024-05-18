using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdatePickUpStoreLogo : IRequest<bool>
    {
        #region Ctor

        public UpdatePickUpStoreLogo(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public UploadedFile File { get; set; } = null!;

        #endregion
    }
}