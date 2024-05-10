namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProduct_ProductCategoryValidator : AbstractValidator<CreateProduct_ProductCategory>
    {
        private readonly IMediator _mediator;

        public CreateProduct_ProductCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .MustAsync(ValidateDoesProduct_ProductCategoryNotExistsAsync)
                    .WithMessage("Ürün zaten bu kategori altında bulunmaktadır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e.ProductId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün bulunamadı.")
                .CustomAsync(ValidateDoesProductExistsAsync);

            RuleFor(e => e.ProductCategoryId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün kategorisi bulunamadı.")
                .CustomAsync(ValidateDoesProductCategoryExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProduct_ProductCategoryNotExistsAsync

        private async Task<bool> ValidateDoesProduct_ProductCategoryNotExistsAsync(
            CreateProduct_ProductCategory value,
            CancellationToken cancellationToken)
        {
            var product_ProductCategoryExists = await _mediator.Send(new DoesProduct_ProductCategoryExists(value.ProductId, value.ProductCategoryId));

            if (product_ProductCategoryExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<CreateProduct_ProductCategory> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProduct_ProductCategory.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductCategoryExistsAsync

        private async Task ValidateDoesProductCategoryExistsAsync(
            long value,
            ValidationContext<CreateProduct_ProductCategory> context,
            CancellationToken cancellationToken)
        {
            var productCategoryExists = await _mediator.Send(new DoesEntityExists<ProductCategoryResponse>(value));

            if (!productCategoryExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProduct_ProductCategory.ProductCategoryId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün kategorisi bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
