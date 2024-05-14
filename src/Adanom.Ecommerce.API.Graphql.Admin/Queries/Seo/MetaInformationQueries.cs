namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class MetaInformationQueries
    {
        #region GetMetaInformationdByIdAsync

        [GraphQLDescription("Gets meta information by id")]
        public async Task<MetaInformationResponse?> GetMetaInformationdByIdAsync(
            long id,
            [Service] IMediator mediator)
        {
            var command = new GetMetaInformation(id);

            return await mediator.Send(command);
        }

        #endregion
    }
}
