using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateImage : IRequest<ImageResponse?>
    {
        #region Ctor

        public CreateImage(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public UploadedFile File { get; set; } = null!;

        public long EntityId { get; set; }

        public bool IsDefault { get; set; }

        public EntityType EntityType { get; set; }

        public int DisplayOrder { get; set; }

        #endregion
    }
}