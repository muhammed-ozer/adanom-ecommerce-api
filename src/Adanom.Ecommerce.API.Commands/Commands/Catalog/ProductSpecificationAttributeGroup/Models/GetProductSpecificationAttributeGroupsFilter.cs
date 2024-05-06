namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetProductSpecificationAttributeGroupsFilter
    {
        public string? Query { get; set; }

        [DefaultValue(GetProductSpecificationAttributeGroupsOrderByEnum.DISPLAY_ORDER_ASC)]
        public GetProductSpecificationAttributeGroupsOrderByEnum? OrderBy { get; set; }
    }
}