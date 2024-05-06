namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductSpecificationAttributeGroupResponse : BaseResponseEntity<long>
    {
        public ProductSpecificationAttributeGroupResponse()
        {
            ProductSpecificationAttributes = new List<ProductSpecificationAttributeResponse>();
        }

        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public IEnumerable<ProductSpecificationAttributeResponse> ProductSpecificationAttributes { get; set; }
    }
}