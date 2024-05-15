using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class AddressCity : BaseEntity<long>
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public byte Code { get; set; }
    }
}
