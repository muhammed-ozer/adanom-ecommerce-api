namespace Adanom.Ecommerce.API.Commands
{
    public class GetSliderItemType : IRequest<SliderItemTypeResponse?>
    {
        #region Ctor

        public GetSliderItemType(SliderItemType sliderItemType)
        {
            SliderItemType = sliderItemType;
        }

        #endregion

        #region Properties

        public SliderItemType SliderItemType { get; set; }

        #endregion
    }
}
