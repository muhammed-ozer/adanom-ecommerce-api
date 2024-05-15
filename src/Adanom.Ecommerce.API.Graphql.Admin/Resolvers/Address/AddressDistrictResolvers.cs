namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(AddressDistrictResponse))]
    public sealed class AddressDistrictResolvers
    {
        #region GetAddressCityAsync

        public async Task<AddressCityResponse?> GetAddressCityAsync(
           [Parent] AddressDistrictResponse addressDistrictResponse,
           [Service] IMediator mediator)
        {
            var addressCity = await mediator.Send(new GetAddressCity(addressDistrictResponse.AddressCityId));

            return addressCity;
        }

        #endregion
    }
}
