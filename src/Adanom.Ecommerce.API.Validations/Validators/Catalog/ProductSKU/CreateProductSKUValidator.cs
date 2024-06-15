using FluentValidation;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProductSKUValidator : AbstractValidator<CreateProductSKU>
    {
        private readonly IMediator _mediator;

        public CreateProductSKUValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Code)
                .NotEmpty()
                    .WithMessage("Ürün stok kodu gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Ürün stok kodu 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                .CustomAsync(ValidateDoesProductSKUCodeNotExistsAsync);

            RuleFor(e => e.StockQuantity)
                .GreaterThanOrEqualTo(0)
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                    .WithMessage("Ürün stok miktarı 0 veya daha fazla olmalıdır.");

            RuleFor(e => e.Barcodes)
                .MaximumLength(1000)
                    .WithMessage("Maksimum 50 barkod eklenebilir.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesProductSKUCodeNotExistsAsync

        private async Task ValidateDoesProductSKUCodeNotExistsAsync(
            string value,
            ValidationContext<CreateProductSKU> context,
            CancellationToken cancellationToken)
        {
            var productSKUCodeExists = await _mediator.Send(new DoesProductSKUCodeExists(value));

            if (productSKUCodeExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProductSKU.Code), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Aynı koda sahip bir başka ürün stok detayı mevcut."
                });
            }
        }

        #endregion

        #endregion
    }
}
