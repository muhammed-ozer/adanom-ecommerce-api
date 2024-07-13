using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(SliderItemResponse))]
    public sealed class SliderItemResolvers
    {
        #region GetImageAsync

        public async Task<ImageResponse?> GetImageAsync(
           [Parent] SliderItemResponse sliderItemResponse,
           [Service] IMediator mediator)
        {
            var images = await mediator.Send(new GetEntityImages(sliderItemResponse.Id, EntityType.SLIDERITEM));

            return images.FirstOrDefault();
        }

        #endregion

        #region GetSliderItemTypeAsync

        public async Task<SliderItemTypeResponse> GetSliderItemTypeAsync(
           [Parent] SliderItemResponse sliderItemResponse,
           [Service] IMediator mediator)
        {
            var sliderItemType = await mediator.Send(new GetSliderItemType(sliderItemResponse.SliderItemType.Key));

            return sliderItemType;
        }

        #endregion
    }
}
