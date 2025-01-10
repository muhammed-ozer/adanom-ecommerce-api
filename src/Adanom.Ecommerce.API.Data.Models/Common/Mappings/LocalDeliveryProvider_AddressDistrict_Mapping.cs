using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(LocalDeliveryProviderId), nameof(AddressDistrictId), IsUnique = true)]
    public class LocalDeliveryProvider_AddressDistrict_Mapping
    {
        public long LocalDeliveryProviderId { get; set; }

        public long AddressDistrictId { get; set; }

        public LocalDeliveryProvider LocalDeliveryProvider { get; set; } = null!;

        public AddressDistrict AddressDistrict { get; set; } = null!;
    }
}
