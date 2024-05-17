using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateShippingProvider : IRequest<bool>
    {
        #region Ctor

        public UpdateShippingProvider(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string DisplayName { get; set; } = null!;

        public decimal MinimumFreeShippingTotalPrice { get; set; }

        public decimal FeeWithoutTax { get; set; }

        public decimal FeeTax { get; set; }

        public decimal FeeTotal { get; set; }

        public byte TaxRate { get; set; }

        public byte ShippingInDays { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        #endregion
    }
}