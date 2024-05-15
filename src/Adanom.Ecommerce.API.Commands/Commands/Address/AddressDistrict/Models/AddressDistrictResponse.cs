namespace Adanom.Ecommerce.API.Commands.Models
{
    public class AddressDistrictResponse : BaseResponseEntity<long>
    {
        public long AddressCityId { get; set; }

        public string Name { get; set; } = null!;

        public AddressCityResponse? AddressCity { get; set; }
    }
}