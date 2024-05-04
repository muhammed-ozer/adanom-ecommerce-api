namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetBrandsFilter
    {
        public string? Query { get; set; }

        [DefaultValue(GetBrandsOrderByEnum.DISPLAY_ORDER_ASC)]
        public GetBrandsOrderByEnum? OrderBy { get; set; }
    }
}