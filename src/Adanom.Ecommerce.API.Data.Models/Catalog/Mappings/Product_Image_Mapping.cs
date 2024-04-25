using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ProductId), nameof(ImageId), IsUnique = true)]
    public class Product_Image_Mapping : IBaseEntity<long>
    {
        public long ImageId { get; set; }

        public long ProductId { get; set; }

        public ImageType ImageType { get; set; }

        public Image Image { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
