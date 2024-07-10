namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public class AddressDistrictQueries
    {
        #region GetAddressDistrictAsync

        [GraphQLDescription("Gets address district")]
        public async Task<AddressDistrictResponse?> GetAddressDistrictAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetAddressDistrict(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetAddressDistrictsAsync

        [GraphQLDescription("Gets address districts")]
        public async Task<IEnumerable<AddressDistrictResponse>> GetAddressDistrictsAsync(
            long? addressCityId,
            [Service] IMediator mediator)
        {
            var command = new GetAddressDistricts(addressCityId);

            return await mediator.Send(command);
        }

        #endregion
    }
}
