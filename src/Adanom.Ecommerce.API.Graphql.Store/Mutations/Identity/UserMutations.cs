namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class UserMutations
    {
        #region UpdateUserPermissionsAsync

        [GraphQLDescription("Updates an user permissions")]
        public async Task<bool> UpdateUserPermissionsAsync(
            UpdateUserPermissionsRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new UpdateUserPermissions(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
