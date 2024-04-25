using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class AddressCity : IBaseEntity<long>
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}
