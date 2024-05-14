using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateImage_Entity : IRequest<bool>
    {
        #region Ctor

        public UpdateImage_Entity(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ImageId { get; set; }

        public bool IsDefault { get; set; }

        public int DisplayOrder { get; set; }

        #endregion
    }
}