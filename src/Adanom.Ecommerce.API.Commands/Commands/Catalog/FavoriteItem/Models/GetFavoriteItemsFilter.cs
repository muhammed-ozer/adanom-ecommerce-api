namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetFavoriteItemsFilter
    {
        public Guid? UserId { get; set; }

        [DefaultValue(GetFavoriteItemsOrderByEnum.CREATED_AT_DESC)]
        public GetFavoriteItemsOrderByEnum? OrderBy { get; set; }
    }
}