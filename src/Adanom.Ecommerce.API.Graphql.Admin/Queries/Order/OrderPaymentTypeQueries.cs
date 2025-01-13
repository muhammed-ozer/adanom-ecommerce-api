namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class OrderPaymentTypeQueries
    {
        #region GetOrderPaymentTypesAsync

        [GraphQLDescription("Gets order payment types")]
        public async Task<IEnumerable<OrderPaymentTypeResponse>> GetOrderPaymentTypesAsync([Service] IMediator mediator)
        {
            var command = new GetOrderPaymentTypes();

            return await mediator.Send(command);
        }

        #endregion
    }
}
