namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateImageRequest
    {
        public long Id { get; set; }

        public bool IsDefault { get; set; }

        public int DisplayOrder { get; set; }
    }
}