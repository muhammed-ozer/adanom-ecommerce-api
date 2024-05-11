namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductSKUBarcodesValidator : AbstractValidator<UpdateProductSKUBarcodes>
    {
        private readonly IMediator _mediator;

        public UpdateProductSKUBarcodesValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün stok detayı bulunamadı.")
                .CustomAsync(ValidateDoesProductSKUExistsAsync);

            RuleFor(e => e.Barcodes)
                .MaximumLength(1000)
                    .WithMessage("Maksimum 50 barkod eklenebilir.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesProductSKUExistsAsync

        private async Task ValidateDoesProductSKUExistsAsync(
            long value,
            ValidationContext<UpdateProductSKUBarcodes> context,
            CancellationToken cancellationToken)
        {
            var productSKUExists = await _mediator.Send(new DoesEntityExists<ProductSKUResponse>(value));

            if (!productSKUExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductSKUBarcodes.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ürün stok detayı bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
