namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        private readonly IMediator _mediator;

        public CreateProductValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.ProductCategoryId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün kategorisi bulunamadı.")
                .CustomAsync(ValidateDoesProductCategoryExistsAsync);

            RuleFor(e => e.BrandId)
                .CustomAsync(ValidateDoesBrandExistsAsync);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Ürün adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Ürün adı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                .CustomAsync(ValidateDoesProductNameNotExistsAsync);

            RuleFor(e => e.Description)
                .MaximumLength(4000)
                    .WithMessage("Ürün açıklaması 4000 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesProductNameNotExistsAsync

        private async Task ValidateDoesProductNameNotExistsAsync(
            string value,
            ValidationContext<CreateProduct> context,
            CancellationToken cancellationToken)
        {
            var productNameExists = await _mediator.Send(new DoesEntityNameExists<ProductResponse>(value));

            if (productNameExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProduct.Name), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu isimde başka bir ürün bulunmaktadır."
                });
            }
        }

        #endregion

        #region ValidateDoesProductCategoryExistsAsync

        private async Task ValidateDoesProductCategoryExistsAsync(
            long value,
            ValidationContext<CreateProduct> context,
            CancellationToken cancellationToken)
        {
            var productCategoryExists = await _mediator.Send(new DoesEntityExists<ProductCategoryResponse>(value));

            if (!productCategoryExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProduct.ProductCategoryId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün kategorisi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesBrandExistsAsync

        private async Task ValidateDoesBrandExistsAsync(
            long? value,
            ValidationContext<CreateProduct> context,
            CancellationToken cancellationToken)
        {
            if (value != null)
            {
                var brandExists = await _mediator.Send(new DoesEntityExists<BrandResponse>(value.Value));

                if (!brandExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateProduct.BrandId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                        ErrorMessage = "Marka bulunamadı."
                    });
                }
            }
        }

        #endregion

        #endregion
    }
}
