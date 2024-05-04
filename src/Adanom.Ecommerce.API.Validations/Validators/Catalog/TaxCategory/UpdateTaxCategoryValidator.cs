namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateTaxCategoryValidator : AbstractValidator<UpdateTaxCategory>
    {
        private readonly IMediator _mediator;

        public UpdateTaxCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Vergi kategorisi bulunamadı.")
                .CustomAsync(ValidateDoesTaxCategoryExistsAsync);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Vergi kategorisi adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Vergi kategorisi adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.GroupName)
                .NotEmpty()
                    .WithMessage("Vergi kategorisi grup adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Vergi kategorisi grup adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Rate)
                .Must(ValidateRateIsBetween0And100)
                .WithMessage("Vergi oranı 0 ile 100 arasında olmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesTaxCategoryExistsAsync

        private async Task ValidateDoesTaxCategoryExistsAsync(
            long value,
            ValidationContext<UpdateTaxCategory> context,
            CancellationToken cancellationToken)
        {
            var taxCategoryExists = await _mediator.Send(new DoesEntityExists<TaxCategoryResponse>(value));

            if (!taxCategoryExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateTaxCategory.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Vergi kategorisi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateRateIsBetween0And100

        private bool ValidateRateIsBetween0And100(byte value)
        {
            return 0 <= value && value <= 100;
        }

        #endregion

        #endregion
    }
}
