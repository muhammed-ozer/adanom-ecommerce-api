namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductCategoryValidator : AbstractValidator<UpdateProductCategory>
    {
        private readonly IMediator _mediator;

        public UpdateProductCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün kategorisi bulunamadı.")
                .CustomAsync(ValidateDoesProductCategoryExistsAsync);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Ürün kategorisi adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Ürün kategorisi adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.ParentId)
                .CustomAsync(ValidateDoesParentProductCategoryExistsAsync);

            RuleFor(e => e)
                .MustAsync(ValidateProductCategoryLevelAllowedAsync)
                    .WithMessage("Kategori katmanları en fazla 3 katmandan oluşmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e)
                .MustAsync(ValidateDoesProductCategoryNameNotExistsAsync)
                    .WithMessage("Bu isimde başka bir ürün kategorisi bulunmaktadır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesProductCategoryExistsAsync

        private async Task ValidateDoesProductCategoryExistsAsync(
            long value,
            ValidationContext<UpdateProductCategory> context,
            CancellationToken cancellationToken)
        {
            var productCategoryExists = await _mediator.Send(new DoesEntityExists<ProductCategoryResponse>(value));

            if (!productCategoryExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductCategory.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün kategorisi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesParentProductCategoryExistsAsync

        private async Task ValidateDoesParentProductCategoryExistsAsync(
            long? value,
            ValidationContext<UpdateProductCategory> context,
            CancellationToken cancellationToken)
        {
            if (value != null)
            {
                var productCatgeoryExists = await _mediator.Send(new DoesEntityExists<ProductCategoryResponse>(value.Value));

                if (!productCatgeoryExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(UpdateProductCategory.Name), null)
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
            UpdateProductCategory value,
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

        #region ValidateDoesProductCategoryNameNotExistsAsync

        private async Task<bool> ValidateDoesProductCategoryNameNotExistsAsync(
            UpdateProductCategory value,
            CancellationToken cancellationToken)
        {
            var productCategoryNameExists = await _mediator.Send(new DoesEntityNameExists<ProductCategoryResponse>(value.Name, value.Id));

            if (productCategoryNameExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}
