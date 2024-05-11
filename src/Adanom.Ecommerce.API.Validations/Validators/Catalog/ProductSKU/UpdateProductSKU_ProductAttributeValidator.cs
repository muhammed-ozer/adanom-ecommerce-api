namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductSKU_ProductAttributeValidator : AbstractValidator<UpdateProductSKU_ProductAttribute>
    {
        private readonly IMediator _mediator;

        public UpdateProductSKU_ProductAttributeValidator(IMediator mediator)
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

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Ürün varyant adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Ürün varyant adı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Value)
                .NotEmpty()
                    .WithMessage("Ürün varyant değeri gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Ürün varyant değeri 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesProductSKUExistsAsync

        private async Task ValidateDoesProductSKUExistsAsync(
            long value,
            ValidationContext<UpdateProductSKU_ProductAttribute> context,
            CancellationToken cancellationToken)
        {
            var productSKUExists = await _mediator.Send(new DoesEntityExists<ProductSKUResponse>(value));

            if (!productSKUExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductSKU_ProductAttribute.Id), null)
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
