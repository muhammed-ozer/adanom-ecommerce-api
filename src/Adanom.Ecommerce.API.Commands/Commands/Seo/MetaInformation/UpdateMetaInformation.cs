using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateMetaInformation : IRequest<MetaInformationResponse?>
    {
        #region Ctor

        public UpdateMetaInformation(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Keywords { get; set; } = null!;

        #endregion
    }
}