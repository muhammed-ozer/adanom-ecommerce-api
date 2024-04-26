using FluentValidation.Results;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class SendPasswordResetEmailValidator : AbstractValidator<SendPasswordResetEmail>
    {
        private readonly IMediator _mediator;

        public SendPasswordResetEmailValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Email)
                .NotEmpty()
                    .WithMessage("E-posta gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .CustomAsync(ValidateDoesUserExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesUserExistsAsync

        private async Task ValidateDoesUserExistsAsync(
            string value,
            ValidationContext<SendPasswordResetEmail> context,
            CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new DoesUserExists(value));

            if (!userExists)
            {
                context.AddFailure(new ValidationFailure(nameof(SendPasswordResetEmail.Email), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Bu e-postaya ait hesap bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
