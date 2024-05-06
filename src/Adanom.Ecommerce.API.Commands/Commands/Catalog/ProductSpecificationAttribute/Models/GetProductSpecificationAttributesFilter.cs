namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetProductSpecificationAttributesFilter
    {
        public string? Query { get; set; }

        public long? ProductSpecificationAttributeGroupId { get; set; }

        [DefaultValue(GetProductSpecificationAttributesOrderByEnum.DISPLAY_ORDER_ASC)]
        public GetProductSpecificationAttributesOrderByEnum? OrderBy { get; set; }
    }
}