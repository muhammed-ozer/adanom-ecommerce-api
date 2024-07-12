using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class OrderShippingAddress : BaseEntity<long>
    {
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [StringLength(500)]
        public string Address { get; set; } = null!;

        public string AddressCityName { get; set; } = null!;

        public string AddressDistrictName { get; set; } = null!;

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;
    }
}
