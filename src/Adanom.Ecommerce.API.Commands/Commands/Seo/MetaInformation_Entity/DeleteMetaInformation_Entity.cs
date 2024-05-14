using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class DeleteMetaInformation_Entity : IRequest<bool>
    {
        #region Ctor

        public DeleteMetaInformation_Entity(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long MetaInformationId { get; set; }

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        #endregion
    }
}