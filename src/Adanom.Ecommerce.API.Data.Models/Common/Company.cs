using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class Company : BaseEntity<long>
    {
        public long AddressCityId { get; set; }

        public long AddressDistrictId { get; set; }

        [StringLength(250)]
        public string LegalName { get; set; } = null!;

        [StringLength(100)]
        public string DisplayName { get; set; } = null!;

        [StringLength(500)]
        public string Address { get; set; } = null!;

        [StringLength(250)]
        public string Email { get; set; } = null!;

        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(100)]
        public string TaxAdministration { get; set; } = null!;

        [StringLength(11)]
        public string TaxNumber { get; set; } = null!;

        [StringLength(50)]
        public string MersisNumber { get; set; } = null!;
    }
}
