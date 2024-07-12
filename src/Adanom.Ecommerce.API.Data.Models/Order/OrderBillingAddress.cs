using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class OrderBillingAddress : BaseEntity<long>
    {
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [StringLength(250)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string Address { get; set; } = null!;

        public string AddressCityName { get; set; } = null!;

        public string AddressDistrictName { get; set; } = null!;

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        public string TaxAdministration { get; set; } = null!;

        [StringLength(11)]
        public string TaxNumber { get; set; } = null!;

        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(250)]
        public string Email { get; set; } = null!;
    }
}
