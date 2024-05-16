namespace Adanom.Ecommerce.API.Commands
{
    public class GetSliderItem : IRequest<SliderItemResponse?>
    {
        #region Ctor

        public GetSliderItem(long id)
        {
            Id = id;
        }

        #endregion

        #region Properties

        public long Id { get; set; }

        #endregion
    }
}