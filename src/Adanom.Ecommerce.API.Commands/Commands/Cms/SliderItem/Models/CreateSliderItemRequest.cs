namespace Adanom.Ecommerce.API.Commands.Models
{
    public class CreateSliderItemRequest
    {
        public UploadedFile File { get; set; } = null!;

        public SliderItemType SliderItemType { get; set; }

        public string Name { get; set; } = null!;

        public string? Url { get; set; }
    }
}