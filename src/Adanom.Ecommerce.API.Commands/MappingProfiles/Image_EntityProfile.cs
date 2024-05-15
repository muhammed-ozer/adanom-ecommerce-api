using Adanom.Ecommerce.API.Business.Models.MappingProfiles.Resolvers;

namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class Image_EntityProfile : Profile
    {
        public Image_EntityProfile()
        {
            CreateMap<UpdateImage_EntityRequest, UpdateImage_Entity>();

            CreateMap<UpdateImage_Entity, Image_Entity_Mapping>();

            CreateMap<Image_Entity_Mapping, ImageResponse>()
                .ForMember(member => member.Id, options => options.MapFrom(source => source.ImageId))
                .ForMember(member => member.Name, options => options.MapFrom(source => source.Image.Name))
                .ForMember(member => member.Path, options => options.MapFrom(source => source.Image.Path))
                .ForMember(member => member.DisplayOrder, options => options.MapFrom(source => source.Image.DisplayOrder));
        }
    }
}