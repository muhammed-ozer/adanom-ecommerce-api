namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class TestQueries
    {
        #region TestAsync

        [GraphQLDescription("Test")]
        public bool GetTestAsync([Service] IMediator mediator)
        {
            return true;
        }

        #endregion
    }
}
