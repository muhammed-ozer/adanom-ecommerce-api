using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateProductSKUInstallment : IRequest<bool>
    {
        #region Ctor

        public UpdateProductSKUInstallment(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public bool IsEligibleToInstallment { get; set; }

        public byte MaximumInstallmentCount { get; set; }

        #endregion
    }
}