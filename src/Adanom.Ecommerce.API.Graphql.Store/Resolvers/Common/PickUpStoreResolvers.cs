using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(PickUpStoreResponse))]
    public sealed class PickUpStoreResolvers
    {
        #region GetLogoAsync

        public async Task<ImageResponse?> GetLogoAsync(
           [Parent] PickUpStoreResponse pickUpStoreResponse,
           [Service] IMediator mediator)
        {
            var logo = await mediator.Send(new GetEntityImage(pickUpStoreResponse.Id, EntityType.PICKUPSTORE, ImageType.LOGO));

            return logo;
        }

        #endregion
    }
}
