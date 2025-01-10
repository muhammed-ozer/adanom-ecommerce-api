namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class LocalDeliveryProvider_AddressDistrictProfile : Profile
    {
        public LocalDeliveryProvider_AddressDistrictProfile()
        {
            CreateMap<CreateLocalDeliveryProvider_AddressDistrictRequest, CreateLocalDeliveryProvider_AddressDistrict>();

            CreateMap<DeleteLocalDeliveryProvider_AddressDistrictRequest, DeleteLocalDeliveryProvider_AddressDistrict>();

            CreateMap<LocalDeliveryProvider_AddressDistrict_Mapping, DeleteLocalDeliveryProvider_AddressDistrictRequest>();
        }
    }
}