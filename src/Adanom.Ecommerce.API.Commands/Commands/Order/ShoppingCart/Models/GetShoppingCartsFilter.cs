namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetShoppingCartsFilter
    {
        [DefaultValue(GetShoppingCartsOrderByEnum.LAST_MODIFIED_AT_DESC)]
        public GetShoppingCartsOrderByEnum? OrderBy { get; set; }
    }
}