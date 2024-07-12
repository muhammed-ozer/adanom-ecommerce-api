namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateOrderShippingAddressRequest
    {
        public string Title { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string AddressCityName { get; set; } = null!;

        public string AddressDistrictName { get; set; } = null!;

        public string? PostalCode { get; set; }

        public string PhoneNumber { get; set; } = null!;
    }
}