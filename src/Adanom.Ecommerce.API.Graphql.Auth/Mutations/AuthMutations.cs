using AutoMapper;

namespace Adanom.Ecommerce.API.Graphql.Auth.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public sealed class AuthMutations
    {
        #region RegisterUserAsync

        [GraphQLDescription("Registers an user")]
        public async Task<bool> RegisterUserAsync(
            RegisterUserRequest request,
            [Service] IMapper mapper,
            [Service] IMediator mediator)
        {
            var command = mapper.Map<RegisterUser>(request);

            return await mediator.Send(command);
        }

        #endregion

        #region SendEmailConfirmationEmailAsync

        [GraphQLDescription("Sends an email confirmation email")]
        public async Task<bool> SendEmailConfirmationEmailAsync(
            SendEmailConfirmationEmailRequest request,
            [Service] IMapper mapper,
            [Service] IMediator mediator)
        {
            var command = mapper.Map<SendEmailConfirmationEmail>(request);

            return await mediator.Send(command);
        }

        #endregion

        #region ConfirmEmailAsync

        [GraphQLDescription("Confirm an user email")]
        public async Task<bool> ConfirmEmailAsync(
            ConfirmEmailRequest request,
            [Service] IMapper mapper,
            [Service] IMediator mediator)
        {
            var command = mapper.Map<ConfirmEmail>(request);

            return await mediator.Send(command);
        }

        #endregion

        #region ChangePasswordAsync

        [GraphQLDescription("Change password")]
        [Authorize]
        public async Task<bool> ChangePasswordAsync(
            ChangePasswordRequest request,
            [Service] IMapper mapper,
            [Service] IMediator mediator,
            [Identity] ClaimsPrincipal identity)
        {
            var command = mapper.Map(request, new ChangePassword(identity));

            return await mediator.Send(command);
        }

        #endregion

        #region SendPasswordResetEmailAsync

        [GraphQLDescription("Sends an email for password reset")]
        public async Task<bool> SendPasswordResetEmailAsync(
            SendPasswordResetEmailRequest request,
            [Service] IMapper mapper,
            [Service] IMediator mediator)
        {
            var command = mapper.Map<SendPasswordResetEmail>(request);

            return await mediator.Send(command);
        }

        #endregion

        #region ResetPasswordAsync

        [GraphQLDescription("Resets an user password")]
        public async Task<bool> ResetPasswordAsync(
            ResetPasswordRequest request,
            [Service] IMapper mapper,
            [Service] IMediator mediator)
        {
            var command = mapper.Map<ResetPassword>(request);

            return await mediator.Send(command);
        }

        #endregion
    }
}