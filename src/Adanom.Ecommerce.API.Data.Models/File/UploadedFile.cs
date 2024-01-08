namespace Adanom.Ecommerce.API.Data.Models
{
    public sealed class UploadedFile
    {
        public string Name { get; set; } = null!;

        public string Extension { get; set; } = null!;

        public byte[] Content { get; set; } = null!;
    }
}
