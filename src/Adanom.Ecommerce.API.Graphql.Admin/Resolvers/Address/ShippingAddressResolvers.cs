namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(ShippingAddressResponse))]
    public sealed class ShippingAddressResolvers
    {
        #region GetAddressCityAsync

        public async Task<AddressCityResponse?> GetAddressCityAsync(
           [Parent] ShippingAddressResponse shippingAddressResponse,
           [Service] IMediator mediator)
        {
            var addressCity = await mediator.Send(new GetAddressCity(shippingAddressResponse.AddressCityId));

            return addressCity;
        }

        #endregion

        #region GetAddressDistrictAsync

        public async Task<AddressDistrictResponse?> GetAddressDistrictAsync(
           [Parent] ShippingAddressResponse shippingAddressResponse,
           [Service] IMediator mediator)
        {
            var addressDistrict = await mediator.Send(new GetAddressDistrict(shippingAddressResponse.AddressDistrictId));

            return addressDistrict;
        }

        #endregion
    }
}
