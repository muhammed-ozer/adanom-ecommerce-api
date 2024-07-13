namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ShippingAddressProfile : Profile
    {
        public ShippingAddressProfile()
        {
            CreateMap<ShippingAddress, ShippingAddressResponse>();

            CreateMap<ShippingAddressResponse, ShippingAddress>();

            CreateMap<CreateShippingAddressRequest, CreateShippingAddress>();

            CreateMap<CreateShippingAddress, ShippingAddress>();

            CreateMap<UpdateShippingAddressRequest, UpdateShippingAddress>();

            CreateMap<UpdateShippingAddress, ShippingAddress>();

            CreateMap<DeleteShippingAddressRequest, DeleteShippingAddress>();
        }
    }
}