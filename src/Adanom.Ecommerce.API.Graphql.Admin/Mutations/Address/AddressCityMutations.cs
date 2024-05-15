namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType("Mutation")]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class AddressCityMutations
    {
        #region ClearAddressCitiesCacheAsync

        [GraphQLDescription("Clears address cities cache")]
        public async Task<bool> ClearAddressCitiesCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<AddressCityResponse>());

            return true;
        }

        #endregion
    }
}
