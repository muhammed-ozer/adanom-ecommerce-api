using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    [Transactional]
    public class CreateProductReview : IRequest<bool>
    {
        #region Ctor

        public CreateProductReview(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductId { get; set; }

        public byte Points { get; set; }

        public string? Comment { get; set; }

        #endregion
    }
}