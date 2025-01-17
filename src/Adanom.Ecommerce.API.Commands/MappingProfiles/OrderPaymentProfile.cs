namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class OrderPaymentProfile : Profile
    {
        public OrderPaymentProfile()
        {
            CreateMap<OrderPayment, OrderPaymentResponse>()
                .ForMember(member => member.OrderPaymentType, options =>
                    options.MapFrom(e => new OrderPaymentTypeResponse(e.OrderPaymentType)));

            CreateMap<OrderPaymentResponse, OrderPayment>()
                .ForMember(member => member.OrderPaymentType, options =>
                    options.MapFrom(e => e.OrderPaymentType.Key));

            CreateMap<CreateOrderPaymentRequest, CreateOrderPayment>();

            CreateMap<CreateOrderPayment, OrderPayment>();

            CreateMap<UpdateOrderPaymentRequest, UpdateOrderPayment>();

            CreateMap<UpdateOrderPayment, OrderPayment>();
        }
    }
}