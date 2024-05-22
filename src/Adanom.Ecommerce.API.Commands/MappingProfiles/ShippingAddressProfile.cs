namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ShippingAddressProfile : Profile
    {
        public ShippingAddressProfile()
        {
            CreateMap<ShippingAddress, ShippingAddressResponse>();

            CreateMap<ShippingAddressResponse, ShippingAddress>();
        }
    }
}