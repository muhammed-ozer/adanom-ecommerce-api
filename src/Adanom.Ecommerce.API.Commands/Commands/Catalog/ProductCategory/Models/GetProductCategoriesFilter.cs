namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetProductCategoriesFilter
    {
        public string? Query { get; set; }

        public long? ParentId { get; set; }

        public ProductCategoryLevel? ProductCategoryLevel { get; set; }

        [DefaultValue(GetProductCategoriesOrderByEnum.DISPLAY_ORDER_ASC)]
        public GetProductCategoriesOrderByEnum? OrderBy { get; set; }
    }
}