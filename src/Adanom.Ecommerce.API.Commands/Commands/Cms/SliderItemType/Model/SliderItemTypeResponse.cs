namespace Adanom.Ecommerce.API.Commands.Models
{
    public sealed class SliderItemTypeResponse
    {
        public SliderItemTypeResponse(SliderItemType key)
        {
            Key = key;
        }

        public SliderItemType Key { get; }

        public string Name { get; set; } = null!;
    }
}