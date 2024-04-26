using FluentValidation.Results;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class SendEmailConfirmationEmailValidator : AbstractValidator<SendEmailConfirmationEmail>
    {
        private readonly IMediator _mediator;

        public SendEmailConfirmationEmailValidator(IMediator mediator)
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
            ValidationContext<SendEmailConfirmationEmail> context,
            CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new DoesUserExists(value));

            if (!userExists)
            {
                context.AddFailure(new ValidationFailure(nameof(SendEmailConfirmationEmail.Email), null)
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
