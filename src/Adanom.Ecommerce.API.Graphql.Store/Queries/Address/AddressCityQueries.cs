namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class AddressCityQueries
    {
        #region GetAddressCityAsync

        [AllowAnonymous]
        [GraphQLDescription("Gets address city")]
        public async Task<AddressCityResponse?> GetAddressCityAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetAddressCity(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetAddressCitiesAsync

        [AllowAnonymous]
        [GraphQLDescription("Gets address cities")]
        public async Task<IEnumerable<AddressCityResponse>> GetAddressCitiesAsync(
            [Service] IMediator mediator)
        {
            var command = new GetAddressCities();

            return await mediator.Send(command);
        }

        #endregion
    }
}
