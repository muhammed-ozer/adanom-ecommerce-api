using Adanom.Ecommerce.API.Data.Models;
using Adanom.Ecommerce.API.Graphql.DataLoaders;

namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(BrandResponse))]
    public sealed class BrandResolvers
    {
        #region GetImagesAsync

        public async Task<IEnumerable<ImageResponse>> GetImagesAsync(
           [Parent] BrandResponse brandResponse,
           [Service] ImagesByEntityDataLoader dataLoader,
           [Service] IMediator mediator,
           [Service] IMapper mapper)
        {
            var images = await dataLoader.LoadAsync((brandResponse.Id, EntityType.BRAND));

            return mapper.Map<List<ImageResponse>>(images);
        }

        #endregion
    }
}
