namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetTaxCategoriesFilter
    {
        public string? Query { get; set; }

        [DefaultValue(GetTaxCategoriesOrderByEnum.RATE_ASC)]
        public GetTaxCategoriesOrderByEnum? OrderBy { get; set; }
    }
}