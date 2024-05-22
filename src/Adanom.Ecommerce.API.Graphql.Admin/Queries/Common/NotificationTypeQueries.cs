namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public sealed class NotificationTypeQueries
    {
        #region GetNotificationTypesAsync

        [GraphQLDescription("Gets notification types")]
        public async Task<IEnumerable<NotificationTypeResponse>> GetNotificationTypesAsync(
            [Service] IMediator mediator)
        {
            return await mediator.Send(new GetNotificationTypes());
        } 

        #endregion
    }
}
