namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ShippingProviderProfile : Profile
    {
        public ShippingProviderProfile()
        {
            CreateMap<ShippingProvider, ShippingProviderResponse>();

            CreateMap<ShippingProviderResponse, ShippingProvider>();

            CreateMap<CreateShippingProviderRequest, CreateShippingProvider>();

            CreateMap<CreateShippingProvider, ShippingProvider>();

            CreateMap<UpdateShippingProviderRequest, UpdateShippingProvider>();

            CreateMap<UpdateShippingProvider, ShippingProvider>();

            CreateMap<UpdateShippingProviderLogoRequest, UpdateShippingProviderLogo>();

            CreateMap<DeleteShippingProviderRequest, DeleteShippingProvider>();
        }
    }
}