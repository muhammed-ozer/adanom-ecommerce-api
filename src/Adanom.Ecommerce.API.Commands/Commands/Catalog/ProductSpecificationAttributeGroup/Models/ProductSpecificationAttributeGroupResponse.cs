namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ProductSpecificationAttributeGroupResponse : BaseResponseEntity<long>
    {
        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}