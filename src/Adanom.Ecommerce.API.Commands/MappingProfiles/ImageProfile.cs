namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageResponse>();

            CreateMap<ImageResponse, Image>();

            CreateMap<CreateImageRequest, CreateImage>();

            CreateMap<CreateImage, Image>()
                .ForMember(member => member.Path, options => options.MapFrom(source => source.File.Name));

            CreateMap<UpdateImageRequest, UpdateImage>();

            CreateMap<UpdateImage, Image>();

            CreateMap<DeleteImageRequest, DeleteImage>();

            CreateMap<DeleteImagesRequest, DeleteImages>();
        }
    }
}