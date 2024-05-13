namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductPriceTaxCategoryValidator : AbstractValidator<UpdateProductPriceTaxCategory>
    {
        private readonly IMediator _mediator;

        public UpdateProductPriceTaxCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün fiyatı bulunamadı.")
                .CustomAsync(ValidateDoesProductPriceExistsAsync);

            RuleFor(e => e.TaxCategoryId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Vergi kategorisi bulunamadı.")
                .CustomAsync(ValidateDoesTaxCategoryExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProductPriceExistsAsync

        private async Task ValidateDoesProductPriceExistsAsync(
            long value,
            ValidationContext<UpdateProductPriceTaxCategory> context,
            CancellationToken cancellationToken)
        {
            var productPriceExists = await _mediator.Send(new DoesEntityExists<ProductPriceResponse>(value));

            if (!productPriceExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductPriceTaxCategory.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ürün fiyatı bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesTaxCategoryExistsAsync

        private async Task ValidateDoesTaxCategoryExistsAsync(
            long value,
            ValidationContext<UpdateProductPriceTaxCategory> context,
            CancellationToken cancellationToken)
        {
            var taxCategoryExists = await _mediator.Send(new DoesEntityExists<TaxCategoryResponse>(value));

            if (!taxCategoryExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductPriceTaxCategory.TaxCategoryId), null)
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
