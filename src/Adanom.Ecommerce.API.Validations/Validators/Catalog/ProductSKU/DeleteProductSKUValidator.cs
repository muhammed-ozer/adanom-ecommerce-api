namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProductSKUValidator : AbstractValidator<DeleteProductSKU>
    {
        private readonly IMediator _mediator;

        public DeleteProductSKUValidator(IMediator mediator)
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
                .CustomAsync(ValidateDoesProductSKUExistsAsync)
                .CustomAsync(ValidateDoesNotProductSKUInUseAsync)
                .CustomAsync(ValidateDoesProductSKUValidToDeleteAsync);
        }

        #region Private Methods

        #region ValidateDoesProductSKUExistsAsync

        private async Task ValidateDoesProductSKUExistsAsync(
            long value,
            ValidationContext<DeleteProductSKU> context,
            CancellationToken cancellationToken)
        {
            var productSKUExists = await _mediator.Send(new DoesEntityExists<ProductSKUResponse>(value));

            if (!productSKUExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductSKU.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ürün stok detayı bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesNotProductSKUInUseAsync

        private async Task ValidateDoesNotProductSKUInUseAsync(
            long value,
            ValidationContext<DeleteProductSKU> context,
            CancellationToken cancellationToken)
        {
            var productSKUInUse = await _mediator.Send(new DoesEntityInUse<ProductSKUResponse>(value));

            if (productSKUInUse)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductSKU.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu ürün stok detayı bir siparişte yer aldığından dolayı silinemez."
                });
            }
        }

        #endregion

        #region ValidateDoesProductSKUValidToDeleteAsync

        private async Task ValidateDoesProductSKUValidToDeleteAsync(
            long value,
            ValidationContext<DeleteProductSKU> context,
            CancellationToken cancellationToken)
        {
            var productSKUValidToDelete = await _mediator.Send(new DoesProductSKUValidToDelete(value));

            if (!productSKUValidToDelete)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductSKU.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu ürün stok detayı ürüne ait başka bir stok detayı olmadığı için silinemez."
                });
            }
        }

        #endregion

        #endregion
    }
}
