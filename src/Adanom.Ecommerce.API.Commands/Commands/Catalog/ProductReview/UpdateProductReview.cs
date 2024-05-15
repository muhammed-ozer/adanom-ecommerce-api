using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductReview : IRequest<bool>
    {
        #region Ctor

        public UpdateProductReview(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public bool IsApproved { get; set; }

        public int DisplayOrder { get; set; }

        #endregion
    }
}