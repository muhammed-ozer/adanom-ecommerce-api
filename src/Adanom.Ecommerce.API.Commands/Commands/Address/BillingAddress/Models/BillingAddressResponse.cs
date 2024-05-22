namespace Adanom.Ecommerce.API.Commands.Models
{
    public class BillingAddressResponse : BaseResponseEntity<long>
    {
        public Guid UserId { get; set; }

        public long AddressCityId { get; set; }

        public long AddressDistrictId { get; set; }

        public long TaxAdministrationId { get; set; }

        public string Title { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? PostalCode { get; set; }

        public string TaxNumber { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsDefault { get; set; }

        public UserResponse? User { get; set; }

        public AddressCityResponse? AddressCity { get; set; }

        public AddressDistrictResponse? AddressDistrict { get; set; }

        public TaxAdministrationResponse? TaxAdministration { get; set; }
    }
}