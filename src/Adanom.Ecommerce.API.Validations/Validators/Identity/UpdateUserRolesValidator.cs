using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateUserRolesValidator : AbstractValidator<UpdateUserRoles>
    {
        private readonly IMediator _mediator;

        public UpdateUserRolesValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .NotNull()
                    .WithMessage(e => "Kullanıcı bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .CustomAsync(ValidateDoesUserExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesUserExistsAsync

        private async Task ValidateDoesUserExistsAsync(
            Guid value,
            ValidationContext<UpdateUserRoles> context,
            CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new DoesUserExists(value));

            if (!userExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateUserRoles.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Kullanıcı bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
