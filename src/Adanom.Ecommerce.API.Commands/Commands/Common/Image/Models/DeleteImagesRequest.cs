namespace Adanom.Ecommerce.API.Commands.Models
{
    public class DeleteImagesRequest
    {
        public long EntityId { get; set; }

        public EntityType EntityType { get; set; }
    }
}