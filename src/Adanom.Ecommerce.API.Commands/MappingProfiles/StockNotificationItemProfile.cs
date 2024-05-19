namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class StockNotificationItemProfile : Profile
    {
        public StockNotificationItemProfile()
        {
            CreateMap<StockNotificationItem, StockNotificationItemResponse>();

            CreateMap<StockNotificationItemResponse, StockNotificationItem>();
        }
    }
}