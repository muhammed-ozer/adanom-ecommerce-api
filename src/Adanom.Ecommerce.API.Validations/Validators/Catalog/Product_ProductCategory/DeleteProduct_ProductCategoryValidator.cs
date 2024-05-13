namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProduct_ProductCategoryValidator : AbstractValidator<DeleteProduct_ProductCategory>
    {
        private readonly IMediator _mediator;

        public DeleteProduct_ProductCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .MustAsync(ValidateDoesProduct_ProductCategoryExistsAsync)
                    .WithMessage("Ürün kategori ilişkisi bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesProduct_ProductCategoryExistsAsync

        private async Task<bool> ValidateDoesProduct_ProductCategoryExistsAsync(
            DeleteProduct_ProductCategory value,
            CancellationToken cancellationToken)
        {
            var product_ProductCategoryExists = await _mediator.Send(new DoesProduct_ProductCategoryExists(value.ProductId, value.ProductCategoryId));

            if (!product_ProductCategoryExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}
