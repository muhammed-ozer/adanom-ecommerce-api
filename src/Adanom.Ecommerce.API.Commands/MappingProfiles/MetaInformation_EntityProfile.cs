namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class MetaInformation_Entity : Profile
    {
        public MetaInformation_Entity()
        {
            CreateMap<CreateMetaInformation_EntityRequest, CreateMetaInformation_Entity>();

            CreateMap<DeleteMetaInformation_EntityRequest, DeleteMetaInformation_Entity>();
        }
    }
}