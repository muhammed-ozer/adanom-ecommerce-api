using Adanom.Ecommerce.API.Business.Models.MappingProfiles.Resolvers;

namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    internal sealed class UploadedFileProfile : Profile
    {
        public UploadedFileProfile()
        {
            CreateMap<IFile, UploadedFile>()
                .ForMember(member => member.Content, options => options.MapFrom(new IFileStreamToByteArrayResolver()))
                .ForMember(member => member.Extension, options => options.MapFrom(source => System.IO.Path.GetExtension(source.Name)));
        }
    }
}
