using Adanom.Ecommerce.API.Data.Models;
using AutoMapper;
using HotChocolate.Types;

namespace Adanom.Ecommerce.API.Business.Models.MappingProfiles.Resolvers
{
    public sealed class IFileStreamToByteArrayResolver : IValueResolver<IFile, UploadedFile, byte[]>
    {
        public byte[] Resolve(IFile source, UploadedFile destination, byte[] member, ResolutionContext context)
        {
            using (var memoryStream = new MemoryStream())
            {
                source.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}
