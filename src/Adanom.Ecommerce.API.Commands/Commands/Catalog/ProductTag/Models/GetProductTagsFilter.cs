namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetProductTagsFilter
    {
        public string? Query { get; set; }

        [DefaultValue(GetProductTagsOrderByEnum.VALUE_ASC)]
        public GetProductTagsOrderByEnum? OrderBy { get; set; }
    }
}