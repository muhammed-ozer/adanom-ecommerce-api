namespace Adanom.Ecommerce.API.Graphql.Store.Resolvers
{
    [ExtendObjectType(typeof(LocalDeliveryProviderResponse))]
    public sealed class LocalDeliveryProviderResolvers
    {
        #region GetSupportedAddressDistrictsAsync

        public async Task<ICollection<AddressDistrictResponse>?> GetSupportedAddressDistrictsAsync(
           [Parent] LocalDeliveryProviderResponse localDeliveryProviderResponse,
           [Service] IMediator mediator)
        {
            var addressDistricts = await mediator.Send(new GetLocalDeliveryProvider_SupportedAddressDistricts(localDeliveryProviderResponse.Id));

            return addressDistricts.ToList();
        }

        #endregion
    }
}
