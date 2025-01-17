using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Graphql.Store.Queries
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

        #region GetAllowedOrderPaymentTypesAsync

        [GraphQLDescription("Gets allowed order payment types")]
        public async Task<IEnumerable<OrderPaymentTypeResponse>> GetAllowedOrderPaymentTypesAsync(
            DeliveryType deliveryType,
            [Service] IMediator mediator)
        {
            var command = new GetAllowedPaymentTypesByDeliveryType(deliveryType);

            return await mediator.Send(command);
        }

        #endregion
    }
}
