using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(BrandId), nameof(ImageId), IsUnique = true)]
    public class Brand_Image_Mapping : IBaseEntity<long>
    {
        public long ImageId { get; set; }

        public long BrandId { get; set; }

        public ImageType ImageType { get; set; }

        public Image Image { get; set; } = null!;

        public Brand Brand { get; set; } = null!;
    }
}
