namespace Adanom.Ecommerce.API.Commands.Models
{
    public class GetStockNotificationItemsFilter
    {
        public Guid? UserId { get; set; }

        [DefaultValue(GetStockNotificationItemsOrderByEnum.CREATED_AT_DESC)]
        public GetStockNotificationItemsOrderByEnum? OrderBy { get; set; }
    }
}