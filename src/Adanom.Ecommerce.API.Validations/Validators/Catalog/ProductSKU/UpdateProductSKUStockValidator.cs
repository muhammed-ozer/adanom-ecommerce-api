namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductSKUStockValidator : AbstractValidator<UpdateProductSKUStock>
    {
        private readonly IMediator _mediator;

        public UpdateProductSKUStockValidator(IMediator mediator)
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

            RuleFor(e => e.StockQuantity)
                .GreaterThanOrEqualTo(0)
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                    .WithMessage("Ürün stok miktarı 0 veya daha fazla olmalıdır.");
        }

        #region Private Methods

        #region ValidateDoesProductSKUExistsAsync

        private async Task ValidateDoesProductSKUExistsAsync(
            long value,
            ValidationContext<UpdateProductSKUStock> context,
            CancellationToken cancellationToken)
        {
            var productSKUExists = await _mediator.Send(new DoesEntityExists<ProductSKUResponse>(value));

            if (!productSKUExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductSKUStock.Id), null)
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
