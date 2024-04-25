using System.ComponentModel.DataAnnotations;

namespace Adanom.Ecommerce.API.Data.Models
{
    public class MetaInformation : IBaseEntity<long>
    {
        public MetaInformation()
        {
            Brand_MetaInformation_Mappings = new List<Brand_MetaInformation_Mapping>();
            ProductCategory_MetaInformation_Mappings = new List<ProductCategory_MetaInformation_Mapping>();
            Product_MetaInformation_Mappings = new List<Product_MetaInformation_Mapping>();
        }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string Description { get; set; } = null!;

        [StringLength(1000)]
        public string Keywords { get; set; } = null!;

        public ICollection<Brand_MetaInformation_Mapping> Brand_MetaInformation_Mappings { get; set; }

        public ICollection<ProductCategory_MetaInformation_Mapping> ProductCategory_MetaInformation_Mappings { get; set; }

        public ICollection<Product_MetaInformation_Mapping> Product_MetaInformation_Mappings { get; set; }
    }
}
