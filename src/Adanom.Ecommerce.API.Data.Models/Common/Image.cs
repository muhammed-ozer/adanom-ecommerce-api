using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class Image : IBaseEntity<long>
    {
        public Image()
        {
            Brand_Image_Mappings = new List<Brand_Image_Mapping>();
            ProductCategory_Image_Mappings = new List<ProductCategory_Image_Mapping>();
            Product_Image_Mappings = new List<Product_Image_Mapping>();
        }

        [StringLength(250)]
        public string Name { get; set; } = null!;

        [StringLength(1000)]
        public string Path { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public Guid? DeletedByUserId { get; set; }

        public ICollection<Brand_Image_Mapping> Brand_Image_Mappings { get; set; }

        public ICollection<ProductCategory_Image_Mapping> ProductCategory_Image_Mappings { get; set; }

        public ICollection<Product_Image_Mapping> Product_Image_Mappings { get; set; }
    }
}
