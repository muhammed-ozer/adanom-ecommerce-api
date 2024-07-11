namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateCompanyRequest
    {
        public long AddressCityId { get; set; }

        public long AddressDistrictId { get; set; }

        public string LegalName { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string TaxAdministration { get; set; } = null!;

        public string TaxNumber { get; set; } = null!;

        public string MersisNumber { get; set; } = null!;
    }
}