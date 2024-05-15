namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageResponse>();

            CreateMap<ImageResponse, Image>();

            CreateMap<CreateImageRequest, CreateImage>();

            CreateMap<CreateImage, Image>();

            CreateMap<UpdateImageRequest, UpdateImage>();

            CreateMap<UpdateImage, Image>();
        }
    }
}