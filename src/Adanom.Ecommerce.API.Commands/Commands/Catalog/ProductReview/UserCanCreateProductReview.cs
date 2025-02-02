using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UserCanCreateProductReview : IRequest<bool>
    {
        #region Ctor

        public UserCanCreateProductReview(ClaimsPrincipal identity, long productId)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            ProductId = productId;
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long ProductId { get; set; }

        #endregion
    }
}