using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class BillingAddress : IBaseEntity<long>
    {
        public Guid UserId { get; set; }

        public long AddressCityId { get; set; }

        public long AddressDistrictId { get; set; }

        public long TaxAdministrationId { get; set; }

        [StringLength(50)]
        public string Title { get; set; } = null!;

        [StringLength(250)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string Address { get; set; } = null!;

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(11)]
        public string TaxNumber { get; set; } = null!;

        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(250)]
        public string Email { get; set; } = null!;

        public bool IsDefault { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public User User { get; set; } = null!;

        public AddressCity AddressCity { get; set; } = null!;

        public AddressDistrict AddressDistrict { get; set; } = null!;

        public TaxAdministration TaxAdministration { get; set; } = null!;
    }
}
