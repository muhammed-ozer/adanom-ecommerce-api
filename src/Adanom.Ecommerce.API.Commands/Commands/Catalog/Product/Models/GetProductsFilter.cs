namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetProductsFilter
    {
        public string? Query { get; set; }

        public long? ProductCategoryId { get; set; }

        public bool? OutOfStock { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsNew { get; set; }

        [DefaultValue(GetProductsOrderByEnum.DISPLAY_ORDER_ASC)]
        public GetProductsOrderByEnum? OrderBy { get; set; }
    }
}