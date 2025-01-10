namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateLocalDeliveryProvider_AddressDistrictRequest
    {
        public long LocalDeliveryProviderId { get; set; }

        public long AddressDistrictId { get; set; }
    }
}