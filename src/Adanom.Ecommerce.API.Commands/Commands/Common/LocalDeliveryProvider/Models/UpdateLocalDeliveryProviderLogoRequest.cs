namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateLocalDeliveryProviderLogoRequest
    {
        public long Id { get; set; }

        public UploadedFile File { get; set; } = null!;
    }
}