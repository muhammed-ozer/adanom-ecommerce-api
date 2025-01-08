using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductPrice_DiscountedPrice : IRequest<bool>
    {
        #region Ctor

        public UpdateProductPrice_DiscountedPrice(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public decimal? DiscountedPrice { get; set; }

        #endregion
    }
}