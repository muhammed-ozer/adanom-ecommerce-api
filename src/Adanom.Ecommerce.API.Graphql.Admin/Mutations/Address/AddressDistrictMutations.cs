namespace Adanom.Ecommerce.API.Graphql.Admin.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class AddressDistrictMutations
    {
        #region ClearAddressDistrictsCacheAsync

        [GraphQLDescription("Clears address districts cache")]
        public async Task<bool> ClearAddressDistrictsCacheAsync([Service] IMediator mediator)
        {
            await mediator.Publish(new ClearEntityCache<AddressDistrictResponse>());

            return true;
        }

        #endregion
    }
}
