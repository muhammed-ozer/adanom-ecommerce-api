namespace Adanom.Ecommerce.API.Commands.Models
{
    public class ImageResponse : BaseResponseEntity<long>
    {
        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public ImageType ImageType { get; set; }

        public string Name { get; set; } = null!;

        public string Path { get; set; } = null!;

        public bool IsDefault { get; set; }

        public int DisplayOrder { get; set; }
    }
}
