namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductPrice_PriceValidator : AbstractValidator<UpdateProductPrice_Price>
    {
        private readonly IMediator _mediator;

        public UpdateProductPrice_PriceValidator(IMediator mediator)
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

            RuleFor(e => e.OriginalPrice)
                .GreaterThanOrEqualTo(0)
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                    .WithMessage("Fiyat sıfır veya daha fazla olmalıdır.");
        }

        #region Private Methods

        #region ValidateDoesProductPriceExistsAsync

        private async Task ValidateDoesProductPriceExistsAsync(
            long value,
            ValidationContext<UpdateProductPrice_Price> context,
            CancellationToken cancellationToken)
        {
            var productPriceExists = await _mediator.Send(new DoesEntityExists<ProductPriceResponse>(value));

            if (!productPriceExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductPrice_Price.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ürün fiyatı bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
