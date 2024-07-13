namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateShippingAddressRequest
    {
        public long Id { get; set; }

        public long AddressCityId { get; set; }

        public long AddressDistrictId { get; set; }

        public string Title { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? PostalCode { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public bool IsDefault { get; set; }
    }
}