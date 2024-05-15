using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteMetaInformation : IRequest<bool>
    {
        #region Ctor

        public DeleteMetaInformation(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        #endregion
    }
}