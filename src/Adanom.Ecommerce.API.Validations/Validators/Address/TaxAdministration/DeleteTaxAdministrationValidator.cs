namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteTaxAdministrationValidator : AbstractValidator<DeleteTaxAdministration>
    {
        private readonly IMediator _mediator;

        public DeleteTaxAdministrationValidator(IMediator mediator)
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
        }

        #region Private Methods

        #region ValidateDoesTaxAdministrationExistsAsync

        private async Task ValidateDoesTaxAdministrationExistsAsync(
            long value,
            ValidationContext<DeleteTaxAdministration> context,
            CancellationToken cancellationToken)
        {
            var taxAdimistrationExists = await _mediator.Send(new DoesEntityExists<TaxAdministrationResponse>(value));

            if (!taxAdimistrationExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteTaxAdministration.Id), null)
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
