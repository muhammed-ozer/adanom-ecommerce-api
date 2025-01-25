namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProduct_ProductSKUValidator : AbstractValidator<CreateProduct_ProductSKU>
    {
        private readonly IMediator _mediator;

        public CreateProduct_ProductSKUValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .MustAsync(ValidateDoesProduct_ProductSKUNotExistsAsync)
                    .WithMessage("Ürün zaten bu SKUya sahip.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e.ProductId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün bulunamadı.")
                .CustomAsync(ValidateDoesProductExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProduct_ProductSKUNotExistsAsync

        private async Task<bool> ValidateDoesProduct_ProductSKUNotExistsAsync(
            CreateProduct_ProductSKU value,
            CancellationToken cancellationToken)
        {
            var product_ProductSKUExists = await _mediator.Send(new DoesProduct_ProductSKUExists(value.ProductId, value.ProductSKUId));

            if (product_ProductSKUExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<CreateProduct_ProductSKU> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProduct_ProductSKU.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
