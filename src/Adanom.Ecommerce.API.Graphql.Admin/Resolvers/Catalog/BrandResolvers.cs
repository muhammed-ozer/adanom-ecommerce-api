using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(BrandResponse))]
    public sealed class BrandResolvers
    {
        #region GetImagesAsync

        public async Task<IEnumerable<ImageResponse>> GetImagesAsync(
           [Parent] BrandResponse brandResponse,
           [Service] IMediator mediator)
        {
            var images = await mediator.Send(new GetEntityImages(brandResponse.Id, EntityType.BRAND));

            return images;
        }

        #endregion
    }
}
