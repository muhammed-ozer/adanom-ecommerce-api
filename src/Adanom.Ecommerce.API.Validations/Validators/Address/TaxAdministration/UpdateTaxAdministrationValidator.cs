namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateTaxAdministrationValidator : AbstractValidator<UpdateTaxAdministration>
    {
        private readonly IMediator _mediator;

        public UpdateTaxAdministrationValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Vergi dairesi bulunamadı.")
                .CustomAsync(ValidateDoesTaxAdministrationExistsAsync);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Vergi dairesi adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(200)
                    .WithMessage("Vergi dairesi adı 200 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesTaxAdministrationExistsAsync

        private async Task ValidateDoesTaxAdministrationExistsAsync(
            long value,
            ValidationContext<UpdateTaxAdministration> context,
            CancellationToken cancellationToken)
        {
            var taxAdministrationExists = await _mediator.Send(new DoesEntityExists<TaxAdministrationResponse>(value));

            if (!taxAdministrationExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateTaxAdministration.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Vergi dairesi bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
