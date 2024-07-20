namespace Adanom.Ecommerce.API.Graphql.Store.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    [Authorize]
    public sealed class ReturnRequestMutations
    {
        #region CreateReturnRequestAsync

        [GraphQLDescription("Creates an return request")]
        public async Task<ReturnRequestResponse?> CreateReturnRequestAsync(
            CreateReturnRequestRequest request,
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new CreateReturnRequest(identity));

            return await mediator.Send(command);
        }

        #endregion
    }
}
