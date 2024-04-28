using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ImageId), IsUnique = true)]
    public class Image_Entity_Mapping : BaseEntity<long>
    {
        public long ImageId { get; set; }

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public ImageType ImageType { get; set; }
    }
}
