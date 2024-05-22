namespace Adanom.Ecommerce.API.Graphql.Admin.Resolvers
{
    [ExtendObjectType(typeof(BillingAddressResponse))]
    public sealed class BillingAddressResolvers
    {
        #region GetAddressCityAsync

        public async Task<AddressCityResponse?> GetAddressCityAsync(
           [Parent] BillingAddressResponse billingAddressResponse,
           [Service] IMediator mediator)
        {
            var addressCity = await mediator.Send(new GetAddressCity(billingAddressResponse.AddressCityId));

            return addressCity;
        }

        #endregion

        #region GetAddressDistrictAsync

        public async Task<AddressDistrictResponse?> GetAddressDistrictAsync(
           [Parent] BillingAddressResponse billingAddressResponse,
           [Service] IMediator mediator)
        {
            var addressDistrict = await mediator.Send(new GetAddressDistrict(billingAddressResponse.AddressDistrictId));

            return addressDistrict;
        }

        #endregion

        #region GetTaxAdministrationAsync

        public async Task<TaxAdministrationResponse?> GetTaxAdministrationAsync(
           [Parent] BillingAddressResponse billingAddressResponse,
           [Service] IMediator mediator)
        {
            var taxAdministration = await mediator.Send(new GetTaxAdministration(billingAddressResponse.TaxAdministrationId));

            return taxAdministration;
        }

        #endregion
    }
}
