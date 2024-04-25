namespace Adanom.Ecommerce.API.Graphql.Store.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class TestQueries
    {
        #region TestAsync

        [GraphQLDescription("Test")]
        public async Task<bool> GetTestAsync(TestModel model, [Service] IMediator mediator)
        {
            await mediator.Send(new Test());
            return true;
        }

        #endregion
    }
}
