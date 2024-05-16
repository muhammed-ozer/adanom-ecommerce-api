namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateTaxAdministrationValidator : AbstractValidator<CreateTaxAdministration>
    {
        private readonly IMediator _mediator;

        public CreateTaxAdministrationValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Code)
                .NotEmpty()
                    .WithMessage("Vergi dairesi kodu gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(50)
                    .WithMessage("Vergi dairesi kodu 50 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                .CustomAsync(ValidateDoesTaxAdministrationCodeNotExistsAsync);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Vergi dairesi adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(200)
                    .WithMessage("Vergi dairesi adı 200 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesTaxAdministrationCodeNotExistsAsync

        private async Task ValidateDoesTaxAdministrationCodeNotExistsAsync(
            string value,
            ValidationContext<CreateTaxAdministration> context,
            CancellationToken cancellationToken)
        {
            var taxAdministrationCodeExists = await _mediator.Send(new DoesTaxAdministrationCodeExists(value));

            if (taxAdministrationCodeExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateTaxAdministration.Code), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Aynı koda sahip bir başka vergi dairesi mevcut."
                });
            }
        }

        #endregion

        #endregion
    }
}
