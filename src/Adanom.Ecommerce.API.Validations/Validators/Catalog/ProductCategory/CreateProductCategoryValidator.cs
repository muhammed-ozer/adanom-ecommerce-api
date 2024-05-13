namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProductCategoryValidator : AbstractValidator<CreateProductCategory>
    {
        private readonly IMediator _mediator;

        public CreateProductCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Ürün kategorisi adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Ürün kategorisi adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                .CustomAsync(ValidateDoesProductCategoryNameNotExistsAsync);

            RuleFor(e => e.ParentId)
                .CustomAsync(ValidateDoesParentProductCategoryExistsAsync);

            RuleFor(e => e)
                .MustAsync(ValidateProductCategoryLevelAllowedAsync)
                    .WithMessage("Kategori katmanları en fazla 3 katmandan oluşmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesProductCategoryNameNotExistsAsync

        private async Task ValidateDoesProductCategoryNameNotExistsAsync(
            string value,
            ValidationContext<CreateProductCategory> context,
            CancellationToken cancellationToken)
        {
            var productCategoryNameExists = await _mediator.Send(new DoesEntityNameExists<ProductCategoryResponse>(value));

            if (productCategoryNameExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProductCategory.Name), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu isimde başka bir ürün kategorisi bulunmaktadır."
                });
            }
        }

        #endregion

        #region ValidateDoesParentProductCategoryExistsAsync

        private async Task ValidateDoesParentProductCategoryExistsAsync(
            long? value,
            ValidationContext<CreateProductCategory> context,
            CancellationToken cancellationToken)
        {
            if (value != null)
            {
                var productCategoryExists = await _mediator.Send(new DoesEntityExists<ProductCategoryResponse>(value.Value));

                if (!productCategoryExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateProductCategory.Name), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Üst ürün kategorisi bulunamadı."
                    });
                }
            }
        }

        #endregion

        #region ValidateProductCategoryLevelAllowedAsync

        private async Task<bool> ValidateProductCategoryLevelAllowedAsync(
            CreateProductCategory value,
            CancellationToken cancellationToken)
        {
            if (value.ProductCategoryLevel == ProductCategoryLevel.THIRD && value.ParentId != null)
            {
                var parentCategory = await _mediator.Send(new GetProductCategory(value.ParentId.Value));

                if (parentCategory != null)
                {
                    if (parentCategory.ProductCategoryLevel == ProductCategoryLevel.THIRD)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #endregion
    }
}
