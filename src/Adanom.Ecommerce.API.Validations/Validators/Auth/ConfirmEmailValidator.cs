namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class ConfirmEmailValidator : AbstractValidator<ConfirmEmail>
    {
        private readonly IMediator _mediator;

        public ConfirmEmailValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Email)
                .NotEmpty()
                    .WithMessage("E-posta gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .CustomAsync(ValidateDoesUserExistsAsync);

            RuleFor(e => e.Token)
                .NotEmpty()
                    .WithMessage("Token bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED);
        }

        #region Private Methods

        #region ValidateDoesUserExistsAsync

        private async Task ValidateDoesUserExistsAsync(
            string value,
            ValidationContext<ConfirmEmail> context,
            CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new DoesUserExists(value));

            if (!userExists)
            {
                context.AddFailure(new ValidationFailure(nameof(ConfirmEmail.Email), null)
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
