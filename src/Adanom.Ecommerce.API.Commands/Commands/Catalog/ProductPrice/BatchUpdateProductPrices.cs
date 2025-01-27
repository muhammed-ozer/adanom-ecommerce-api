using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class BatchUpdateProductPrices : IRequest<bool>
    {
        #region Ctor

        public BatchUpdateProductPrices(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public UploadedFile File { get; set; } = null!;

        #endregion
    }
}