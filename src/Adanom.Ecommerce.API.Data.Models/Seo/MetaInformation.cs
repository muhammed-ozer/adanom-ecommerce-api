using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class MetaInformation : IBaseEntity<long>
    {
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string Description { get; set; } = null!;

        [StringLength(1000)]
        public string Keywords { get; set; } = null!;
    }
}
