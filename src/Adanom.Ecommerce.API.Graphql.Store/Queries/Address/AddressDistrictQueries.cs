namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class AddressDistrictQueries
    {
        #region GetAddressDistrictAsync

        [AllowAnonymous]
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

        [AllowAnonymous]
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
