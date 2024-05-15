namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateImageRequest
    {
        public UploadedFile File { get; set; } = null!;

        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public bool IsDefault { get; set; }

        public int DisplayOrder { get; set; }
    }
}