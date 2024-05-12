namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProductAttributeValidator : AbstractValidator<DeleteProductAttribute>
    {
        private readonly IMediator _mediator;

        public DeleteProductAttributeValidator(IMediator mediator)
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
        }

        #region Private Methods

        #region ValidateDoesProductAttributeExistsAsync

        private async Task ValidateDoesProductAttributeExistsAsync(
            long value,
            ValidationContext<DeleteProductAttribute> context,
            CancellationToken cancellationToken)
        {
            var productAttributeExists = await _mediator.Send(new DoesEntityExists<ProductAttributeResponse>(value));

            if (!productAttributeExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductAttribute.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ürün varyantı bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
