namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemResponse>();

            CreateMap<OrderItemResponse, OrderItem>();
        }
    }
}