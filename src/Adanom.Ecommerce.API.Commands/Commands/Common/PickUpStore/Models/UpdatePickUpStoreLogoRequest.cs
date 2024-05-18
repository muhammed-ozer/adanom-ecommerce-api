namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdatePickUpStoreLogoRequest
    {
        public long Id { get; set; }

        public UploadedFile File { get; set; } = null!;
    }
}