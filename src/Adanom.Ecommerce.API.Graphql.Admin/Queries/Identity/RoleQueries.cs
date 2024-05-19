namespace Adanom.Ecommerce.API.Graphql.Admin.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    // TODO: Implement authorize [Authorize(Policy = SecurityConstants.Policies.Admin.Name)]
    public sealed class RoleQueries
    {
        #region GetRoleByIdAsync

        [GraphQLDescription("Gets a role by id")]
        public async Task<RoleResponse?> GetRoleByIdAsync(
            Guid id,
            [Service] IMediator mediator)
        {
            var command = new GetRole(id);

            return await mediator.Send(command);
        }

        #endregion

        #region GetRoleByNameAsync

        [GraphQLDescription("Gets a role by name")]
        public async Task<RoleResponse?> GetRoleByNameAsync(
            string name,
            [Service] IMediator mediator)
        {
            var command = new GetRole(name);

            return await mediator.Send(command);
        }

        #endregion

        #region GetRolesAsync

        [GraphQLDescription("Gets roles")]
        public async Task<IEnumerable<RoleResponse>> GetRolesAsync([Service] IMediator mediator)
        {
            var command = new GetRoles();

            return await mediator.Send(command);
        }

        #endregion
    }
}
