namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class CompanyQueries
    {
        #region GetCompanyAsync

        [AllowAnonymous]
        [GraphQLDescription("Gets a company")]
        public async Task<CompanyResponse?> GetCompanyAsync([Service] IMediator mediator)
        {
            var command = new GetCompany();

            return await mediator.Send(command);
        }

        #endregion
    }
}
