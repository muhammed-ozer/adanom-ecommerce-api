namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProductSpecificationAttributeValidator : AbstractValidator<DeleteProductSpecificationAttribute>
    {
        private readonly IMediator _mediator;

        public DeleteProductSpecificationAttributeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün özelliği bulunamadı.")
                .CustomAsync(ValidateDoesProductSpecificationAttributeExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProductSpecificationAttributeExistsAsync

        private async Task ValidateDoesProductSpecificationAttributeExistsAsync(
            long value,
            ValidationContext<DeleteProductSpecificationAttribute> context,
            CancellationToken cancellationToken)
        {
            var productSpecificationAttributeExists = await _mediator.Send(new DoesEntityExists<ProductSpecificationAttributeResponse>(value));

            if (!productSpecificationAttributeExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductSpecificationAttribute.Id), null)
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
