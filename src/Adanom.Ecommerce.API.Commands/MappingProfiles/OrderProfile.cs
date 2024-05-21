namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(member => member.OrderStatusType, options =>
                    options.MapFrom(e => new OrderStatusTypeResponse(e.OrderStatusType)))
                .ForMember(member => member.DeliveryType, options =>
                    options.MapFrom(e => new DeliveryTypeResponse(e.DeliveryType)));

            CreateMap<OrderResponse, Order>()
                .ForMember(member => member.OrderStatusType, options => 
                    options.MapFrom(e => e.OrderStatusType!.Key))
                .ForMember(member => member.DeliveryType, options =>
                    options.MapFrom(e => e.DeliveryType!.Key));

            CreateMap<UpdateOrder_OrderStatusTypeRequest, UpdateOrder_OrderStatusType>();

            CreateMap<UpdateOrder_OrderStatusType, Order>()
                .ForMember(member => member.OrderStatusType, options =>
                    options.MapFrom(e => e.NewOrderStatusType));
        }
    }
}