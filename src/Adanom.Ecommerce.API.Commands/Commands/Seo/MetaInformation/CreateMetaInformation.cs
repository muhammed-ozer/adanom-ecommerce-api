using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class CreateMetaInformation : IRequest<MetaInformationResponse?>
    {
        #region Ctor

        public CreateMetaInformation(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Keywords { get; set; } = null!;

        #endregion
    }
}