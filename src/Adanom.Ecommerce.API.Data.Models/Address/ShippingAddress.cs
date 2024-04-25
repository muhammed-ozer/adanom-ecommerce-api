using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class ShippingAddress : IBaseEntity<long>
    {
        public Guid UserId { get; set; }

        public long AddressCityId { get; set; }

        public long AddressDistrictId { get; set; }

        [StringLength(50)]
        public string Title { get; set; } = null!;

        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [StringLength(500)]
        public string Address { get; set; } = null!;

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;

        public bool IsDefault { get; set; }

        public User User { get; set; } = null!;

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public AddressCity AddressCity { get; set; } = null!;

        public AddressDistrict AddressDistrict { get; set; } = null!;
    }
}
