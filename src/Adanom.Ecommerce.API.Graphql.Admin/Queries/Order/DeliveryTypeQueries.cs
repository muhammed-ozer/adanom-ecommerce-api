namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class DeliveryTypeQueries
    {
        #region GetDeliveryTypesAsync

        [GraphQLDescription("Gets delivery types")]
        public async Task<IEnumerable<DeliveryTypeResponse>> GetDeliveryTypesAsync([Service] IMediator mediator)
        {
            var command = new GetDeliveryTypes();

            return await mediator.Send(command);
        }

        #endregion
    }
}
