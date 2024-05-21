namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class OrderPaymentProfile : Profile
    {
        public OrderPaymentProfile()
        {
            CreateMap<OrderPayment, OrderPaymentResponse>();

            CreateMap<OrderPaymentResponse, OrderPayment>();
        }
    }
}