namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateBillingAddressRequest
    {
        public long Id { get; set; }

        public long AddressCityId { get; set; }

        public long AddressDistrictId { get; set; }

        public string Title { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? PostalCode { get; set; }

        public string TaxAdministration { get; set; } = null!;

        public string TaxNumber { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsDefault { get; set; }
    }
}