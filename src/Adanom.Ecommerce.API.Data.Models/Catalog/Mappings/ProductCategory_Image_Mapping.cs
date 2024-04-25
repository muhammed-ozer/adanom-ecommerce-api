using Microsoft.EntityFrameworkCore;

namespace Adanom.Ecommerce.API.Data.Models
{
    [Index(nameof(ProductCategoryId), nameof(ImageId), IsUnique = true)]
    public class ProductCategory_Image_Mapping : IBaseEntity<long>
    {
        public long ImageId { get; set; }

        public long ProductCategoryId { get; set; }

        public ImageType ImageType { get; set; }

        public Image Image { get; set; } = null!;

        public ProductCategory ProductCategory { get; set; } = null!;
    }
}
