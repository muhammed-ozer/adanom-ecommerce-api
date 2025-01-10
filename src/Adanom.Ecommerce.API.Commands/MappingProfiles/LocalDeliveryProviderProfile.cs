namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class LocalDeliveryProviderProfile : Profile
    {
        public LocalDeliveryProviderProfile()
        {
            CreateMap<LocalDeliveryProvider, LocalDeliveryProviderResponse>();

            CreateMap<LocalDeliveryProviderResponse, LocalDeliveryProvider>();

            CreateMap<CreateLocalDeliveryProviderRequest, CreateLocalDeliveryProvider>();

            CreateMap<CreateLocalDeliveryProvider, LocalDeliveryProvider>();

            CreateMap<UpdateLocalDeliveryProviderRequest, UpdateLocalDeliveryProvider>();

            CreateMap<UpdateLocalDeliveryProvider, LocalDeliveryProvider>();

            CreateMap<UpdateLocalDeliveryProviderLogoRequest, UpdateLocalDeliveryProviderLogo>();

            CreateMap<DeleteLocalDeliveryProviderRequest, DeleteLocalDeliveryProvider>();
        }
    }
}