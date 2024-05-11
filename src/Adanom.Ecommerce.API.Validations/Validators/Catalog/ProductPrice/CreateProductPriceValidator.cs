namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProductPriceValidator : AbstractValidator<CreateProductPrice>
    {
        private readonly IMediator _mediator;

        public CreateProductPriceValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.OriginalPrice)
                .GreaterThanOrEqualTo(0)
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                    .WithMessage("Fiyat sıfır veya daha fazla olmalıdır.");

            RuleFor(e => e.TaxCategoryId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Vergi kategorisi bulunamadı.")
                .CustomAsync(ValidateDoesTaxCategoryExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesTaxCategoryExistsAsync

        private async Task ValidateDoesTaxCategoryExistsAsync(
            long value,
            ValidationContext<CreateProductPrice> context,
            CancellationToken cancellationToken)
        {
            var taxCategoryExists = await _mediator.Send(new DoesEntityExists<TaxCategoryResponse>(value));

            if (!taxCategoryExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProductPrice.TaxCategoryId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Vergi kategorisi bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
