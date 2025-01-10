using System.Security.Claims;

namespace Adanom.Ecommerce.API.Commands
{
    public class UpdateLocalDeliveryProvider : IRequest<bool>
    {
        #region Ctor

        public UpdateLocalDeliveryProvider(ClaimsPrincipal identity)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        #endregion

        #region Properties

        public ClaimsPrincipal Identity { get; }

        public long Id { get; set; }

        public string DisplayName { get; set; } = null!;

        public decimal MinimumOrderGrandTotal { get; set; }

        public decimal MinimumFreeDeliveryOrderGrandTotal { get; set; }

        public decimal FeeTotal { get; set; }

        public byte TaxRate { get; set; }

        public byte DeliveryInHours { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public ICollection<long> SupportedAddressDistrictIds { get; set; } = new List<long>();

        #endregion
    }
}