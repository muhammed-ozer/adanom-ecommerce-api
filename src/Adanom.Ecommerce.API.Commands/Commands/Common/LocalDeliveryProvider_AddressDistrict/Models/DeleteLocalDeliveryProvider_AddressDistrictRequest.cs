namespace Adanom.Ecommerce.API.Commands.Models
{
    public class DeleteLocalDeliveryProvider_AddressDistrictRequest
    {
        public long LocalDeliveryProviderId { get; set; }

        public long? AddressDistrictId { get; set; }
    }
}