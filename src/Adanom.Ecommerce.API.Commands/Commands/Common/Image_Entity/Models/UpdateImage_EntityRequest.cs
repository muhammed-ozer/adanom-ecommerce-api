namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateImage_EntityRequest
    {
        public long ImageId { get; set; }

        public bool IsDefault { get; set; }

        public int DisplayOrder { get; set; }
    }
}