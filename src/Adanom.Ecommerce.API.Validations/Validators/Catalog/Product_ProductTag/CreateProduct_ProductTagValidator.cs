namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProduct_ProductTagValidator : AbstractValidator<CreateProduct_ProductTag>
    {
        private readonly IMediator _mediator;

        public CreateProduct_ProductTagValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .MustAsync(ValidateDoesProduct_ProductTagNotExistsAsync)
                    .WithMessage("Ürün zaten bu ürün etiketine sahip.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e.ProductId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün bulunamadı.")
                .CustomAsync(ValidateDoesProductExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProduct_ProductTagNotExistsAsync

        private async Task<bool> ValidateDoesProduct_ProductTagNotExistsAsync(
            CreateProduct_ProductTag value,
            CancellationToken cancellationToken)
        {
            var product_ProductTagExists = await _mediator.Send(new DoesProduct_ProductTagExists(value.ProductId, value.ProductTag_Value));

            if (product_ProductTagExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<CreateProduct_ProductTag> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProduct_ProductTag.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
