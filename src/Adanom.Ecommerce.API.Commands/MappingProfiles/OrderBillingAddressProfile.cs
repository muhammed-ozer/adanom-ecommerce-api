namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class OrderBillingAddressProfile : Profile
    {
        public OrderBillingAddressProfile()
        {
            CreateMap<OrderBillingAddress, OrderBillingAddressResponse>();

            CreateMap<OrderBillingAddressResponse, OrderBillingAddress>();

            CreateMap<BillingAddressResponse, CreateOrderBillingAddressRequest>();

            CreateMap<CreateOrderBillingAddressRequest, CreateOrderBillingAddress>();

            CreateMap<CreateOrderBillingAddress, OrderBillingAddress>();
        }
    }
}