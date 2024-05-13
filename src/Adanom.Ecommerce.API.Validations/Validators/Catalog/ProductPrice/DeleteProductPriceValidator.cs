namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProductPriceValidator : AbstractValidator<DeleteProductPrice>
    {
        private readonly IMediator _mediator;

        public DeleteProductPriceValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün fiyatı bulunamadı.")
                .CustomAsync(ValidateDoesProductPriceExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProductPriceExistsAsync

        private async Task ValidateDoesProductPriceExistsAsync(
            long value,
            ValidationContext<DeleteProductPrice> context,
            CancellationToken cancellationToken)
        {
            var productPriceExists = await _mediator.Send(new DoesEntityExists<ProductPriceResponse>(value));

            if (!productPriceExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductPrice.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ürün fiyatı bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
