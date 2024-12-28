using Adanom.Ecommerce.API.Commands.Models;
using Adanom.Ecommerce.API.Data.Models;
using Adanom.Ecommerce.API.Graphql.DataLoaders;

namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(SliderItemResponse))]
    public sealed class SliderItemResolvers
    {
        #region GetImageAsync

        public async Task<ImageResponse?> GetImageAsync(
           [Parent] SliderItemResponse sliderItemResponse,
           [Service] EntityImageDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var image = await dataLoader.LoadAsync((sliderItemResponse.Id, EntityType.SLIDERITEM));

            return mapper.Map<ImageResponse>(image);
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
