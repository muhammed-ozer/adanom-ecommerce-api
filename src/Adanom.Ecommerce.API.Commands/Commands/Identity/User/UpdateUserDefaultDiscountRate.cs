using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateUserDefaultDiscountRate : IRequest<bool>
    {
        #region Ctor

        public UpdateUserDefaultDiscountRate(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public Guid Id { get; set; }

        public decimal DefaultDiscountRate { get; set; }

        #endregion
    }
}