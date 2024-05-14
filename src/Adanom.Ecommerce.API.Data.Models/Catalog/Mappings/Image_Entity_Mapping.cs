using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ImageId), IsUnique = true)]
    public class Image_Entity_Mapping
    {
        public long ImageId { get; set; }

        public long EntityId { get; set; }

        public bool IsDefault { get; set; }

        public EntityType EntityType { get; set; }

        [NotMapped]
        public Image Image { get; set; } = null!;
    }
}
