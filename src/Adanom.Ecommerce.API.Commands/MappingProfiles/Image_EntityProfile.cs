namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class Image_EntityProfile : Profile
    {
        public Image_EntityProfile()
        {
            CreateMap<UpdateImage_EntityRequest, UpdateImage_Entity>();

            CreateMap<UpdateImage_Entity, Image_Entity_Mapping>();
        }
    }
}