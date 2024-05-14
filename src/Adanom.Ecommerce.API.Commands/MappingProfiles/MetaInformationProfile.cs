namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class MetaInformationProfile : Profile
    {
        public MetaInformationProfile()
        {
            CreateMap<MetaInformation, MetaInformationResponse>();

            CreateMap<MetaInformationResponse, MetaInformation>();

            CreateMap<CreateMetaInformationRequest, CreateMetaInformation>();

            CreateMap<CreateMetaInformation, MetaInformation>();

            CreateMap<UpdateMetaInformationRequest, UpdateMetaInformation>();

            CreateMap<UpdateMetaInformation, MetaInformation>();
        }
    }
}