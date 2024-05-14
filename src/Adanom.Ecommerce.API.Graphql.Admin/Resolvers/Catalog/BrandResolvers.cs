using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(BrandResponse))]
    public sealed class BrandResolvers
    {
        #region GetMetaInformationAsync

        public async Task<MetaInformationResponse?> GetMetaInformationAsync(
           [Parent] BrandResponse brandResponse,
           [Service] IMediator mediator)
        {
            var metaInformation = await mediator.Send(new GetMetaInformation(brandResponse.Id, EntityType.BRAND));

            return metaInformation;
        }

        #endregion
    }
}
