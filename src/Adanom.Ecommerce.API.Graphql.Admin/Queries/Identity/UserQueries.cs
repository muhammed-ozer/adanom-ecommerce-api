namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class UserQueries
    {
        #region GetUserByIdAsync

        [GraphQLDescription("Gets an user by id")]
        public async Task<UserResponse?> GetUserByIdAsync(
            Guid id,
            [Service] IMediator mediator)
        {
            var command = new GetUser(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetUserByEmailAsync

        [GraphQLDescription("Gets an user by email")]
        public async Task<UserResponse?> GetUserByEmailAsync(
            string email,
            [Service] IMediator mediator)
        {
            var command = new GetUser(email);

            return await mediator.Send(command);
        }

        #endregion

        #region GetUsersAsync

        [GraphQLDescription("Gets users")]
        public async Task<PaginatedData<UserResponse>> GetUsersAsync(
            GetUsersFilter? filter,
            PaginationRequest? paginationRequest,
            [Service] IMediator mediator)
        {
            var command = new GetUsers(filter, paginationRequest);

            return await mediator.Send(command);
        }

        #endregion
    }
}
