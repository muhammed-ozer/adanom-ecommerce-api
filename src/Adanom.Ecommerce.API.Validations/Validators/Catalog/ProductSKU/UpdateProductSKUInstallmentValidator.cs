namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductSKUInstallmentValidator : AbstractValidator<UpdateProductSKUInstallment>
    {
        private readonly IMediator _mediator;

        public UpdateProductSKUInstallmentValidator(IMediator mediator)
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
        }

        #region Private Methods

        #region ValidateDoesProductSKUExistsAsync

        private async Task ValidateDoesProductSKUExistsAsync(
            long value,
            ValidationContext<UpdateProductSKUInstallment> context,
            CancellationToken cancellationToken)
        {
            var productSKUExists = await _mediator.Send(new DoesEntityExists<ProductSKUResponse>(value));

            if (!productSKUExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductSKUInstallment.Id), null)
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
