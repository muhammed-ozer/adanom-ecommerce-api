namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProductValidator : AbstractValidator<DeleteProduct>
    {
        private readonly IMediator _mediator;

        public DeleteProductValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün bulunamadı.")
                .CustomAsync(ValidateDoesProductExistsAsync)
                .CustomAsync(ValidateDoesNotProductInUseAsync);
        }

        #region Private Methods

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<DeleteProduct> context,
            CancellationToken cancellationToken)
        {
            var brandExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!brandExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProduct.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesNotProductInUseAsync

        private async Task ValidateDoesNotProductInUseAsync(
            long value,
            ValidationContext<DeleteProduct> context,
            CancellationToken cancellationToken)
        {
            var productInUse = await _mediator.Send(new DoesEntityInUse<ProductResponse>(value));

            if (productInUse)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProduct.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu ürün bir siparişte yer aldığından dolayı silinemez. Ürünü pasif hale getirebilirsiniz."
                });
            }
        }

        #endregion

        #endregion
    }
}
