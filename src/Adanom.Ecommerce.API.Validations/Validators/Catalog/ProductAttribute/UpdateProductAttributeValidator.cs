namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductAttributeValidator : AbstractValidator<UpdateProductAttribute>
    {
        private readonly IMediator _mediator;

        public UpdateProductAttributeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün varyantı bulunamadı.")
                .CustomAsync(ValidateDoesProductAttributeExistsAsync);

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

        #region ValidateDoesProductAttributeExistsAsync

        private async Task ValidateDoesProductAttributeExistsAsync(
            long value,
            ValidationContext<UpdateProductAttribute> context,
            CancellationToken cancellationToken)
        {
            var productAttributeExists = await _mediator.Send(new DoesEntityExists<ProductAttributeResponse>(value));

            if (!productAttributeExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductAttribute.Id), null)
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
