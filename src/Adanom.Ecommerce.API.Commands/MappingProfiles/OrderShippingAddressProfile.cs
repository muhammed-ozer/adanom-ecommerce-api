namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class OrderShippingAddressProfile : Profile
    {
        public OrderShippingAddressProfile()
        {
            CreateMap<OrderShippingAddress, OrderShippingAddressResponse>();

            CreateMap<OrderShippingAddressResponse, OrderShippingAddress>();

            CreateMap<ShippingAddressResponse, CreateOrderShippingAddressRequest>();

            CreateMap<CreateOrderShippingAddressRequest, CreateOrderShippingAddress>();

            CreateMap<CreateOrderShippingAddress, OrderShippingAddress>();
        }
    }
}