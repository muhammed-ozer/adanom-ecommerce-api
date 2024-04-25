using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class AddressDistrict : IBaseEntity<long>
    {
        public long AddressCityId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        public DateTime? CreatedAtUtc { get; set; }

        public Guid? CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public AddressCity AddressCity { get; set; } = null!;

        public User? CreatedByUser { get; set; }

        public User? UpdatedByUser { get; set; }
    }
}