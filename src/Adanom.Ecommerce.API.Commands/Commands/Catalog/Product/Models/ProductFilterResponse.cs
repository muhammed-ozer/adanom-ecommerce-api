namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductFilterResponse
    {
        public ProductFilterResponse()
        {
            Brands = new List<BrandResponse>();
            ProductSpecificationAttributes = new List<ProductSpecificationAttributeResponse>();
            ProductCategories = new List<ProductCategoryResponse>();
        }

        public IEnumerable<BrandResponse> Brands { get; set; }

        public IEnumerable<ProductSpecificationAttributeResponse> ProductSpecificationAttributes { get; set; }

        public IEnumerable<ProductCategoryResponse> ProductCategories { get; set; }

        public decimal MinimumPrice { get; set; }

        public decimal MaximumPrice { get; set; }
    }
}