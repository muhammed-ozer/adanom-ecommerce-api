namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteTaxCategoryValidator : AbstractValidator<DeleteTaxCategory>
    {
        private readonly IMediator _mediator;

        public DeleteTaxCategoryValidator(IMediator mediator)
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
                .CustomAsync(ValidateDoesTaxCategoryExistsAsync)
                .CustomAsync(ValidateDoesNotTaxCategoryInUseAsync);
        }

        #region Private Methods

        #region ValidateDoesTaxCategoryExistsAsync

        private async Task ValidateDoesTaxCategoryExistsAsync(
            long value,
            ValidationContext<DeleteTaxCategory> context,
            CancellationToken cancellationToken)
        {
            var taxCategoryExists = await _mediator.Send(new DoesEntityExists<TaxCategoryResponse>(value));

            if (!taxCategoryExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteTaxCategory.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Vergi kategorisi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesNotTaxCategoryInUseAsync

        private async Task ValidateDoesNotTaxCategoryInUseAsync(
            long value,
            ValidationContext<DeleteTaxCategory> context,
            CancellationToken cancellationToken)
        {
            var taxCategoryInUse = await _mediator.Send(new DoesEntityInUse<TaxCategoryResponse>(value));

            if (taxCategoryInUse)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteTaxCategory.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu vergi kategorisi altında ürünler olduğundan dolayı silinemez."
                });
            }
        }

        #endregion

        #endregion
    }
}
