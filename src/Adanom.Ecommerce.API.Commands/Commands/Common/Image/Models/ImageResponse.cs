namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ImageResponse : BaseResponseEntity<long>
    {
        public string Name { get; set; } = null!;

        public string Path { get; set; } = null!;

        public int DisplayOrder { get; set; }

        public ImageType ImageType { get; set; }
    }
}
