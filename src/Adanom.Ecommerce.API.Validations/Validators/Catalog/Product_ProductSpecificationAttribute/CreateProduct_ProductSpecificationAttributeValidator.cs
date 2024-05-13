namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProduct_ProductSpecificationAttributeValidator : AbstractValidator<CreateProduct_ProductSpecificationAttribute>
    {
        private readonly IMediator _mediator;

        public CreateProduct_ProductSpecificationAttributeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .MustAsync(ValidateDoesProduct_ProductSpecificationAttributeNotExistsAsync)
                    .WithMessage("Ürün zaten bu ürün özelliğine sahip.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e.ProductId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün bulunamadı.")
                .CustomAsync(ValidateDoesProductExistsAsync);

            RuleFor(e => e.ProductSpecificationAttributeId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün özelliği bulunamadı.")
                .CustomAsync(ValidateDoesProductSpecificationAttributeExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProduct_ProductSpecificationAttributeNotExistsAsync

        private async Task<bool> ValidateDoesProduct_ProductSpecificationAttributeNotExistsAsync(
            CreateProduct_ProductSpecificationAttribute value,
            CancellationToken cancellationToken)
        {
            var product_ProductSpecificationAttributeExists = await _mediator.Send(new DoesProduct_ProductSpecificationAttributeExists(value.ProductId, value.ProductSpecificationAttributeId));

            if (product_ProductSpecificationAttributeExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<CreateProduct_ProductSpecificationAttribute> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProduct_ProductSpecificationAttribute.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductSpecificationAttributeExistsAsync

        private async Task ValidateDoesProductSpecificationAttributeExistsAsync(
            long value,
            ValidationContext<CreateProduct_ProductSpecificationAttribute> context,
            CancellationToken cancellationToken)
        {
            var ProductSpecificationAttributeExists = await _mediator.Send(new DoesEntityExists<ProductSpecificationAttributeResponse>(value));

            if (!ProductSpecificationAttributeExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProduct_ProductSpecificationAttribute.ProductSpecificationAttributeId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün özelliği bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
