namespace Adanom.Ecommerce.API.Commands.Models
{
    public class UpdateBrandLogoRequest
    {
        public long Id { get; set; }

        public UploadedFile Logo { get; set; } = null!;
    }
}