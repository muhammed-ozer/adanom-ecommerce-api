namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetAnonymousShoppingCartsFilter
    {
        [DefaultValue(GetAnonymousShoppingCartsOrderByEnum.LAST_MODIFIED_AT_DESC)]
        public GetAnonymousShoppingCartsOrderByEnum? OrderBy { get; set; }
    }
}