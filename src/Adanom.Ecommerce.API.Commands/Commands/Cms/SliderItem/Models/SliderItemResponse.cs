namespace Adanom.Ecommerce.API.Commands.Models
{
    public class SliderItemResponse : BaseResponseEntity<long>
    {
        public string Name { get; set; } = null!;

        public string? Url { get; set; }

        public SliderItemTypeResponse SliderItemType { get; set; } = null!;

        public ImageResponse? Image { get; set; }
    }
}