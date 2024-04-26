namespace Adanom.Ecommerce.API.Graphql.Auth.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TestQueries
    {
        #region TestAsync

        [GraphQLDescription("Test")]
        public bool GetTestAsync([Service] IMediator mediator, [Identity] ClaimsPrincipal identity)
        {
            return true;
        }

        #endregion
    }
}
