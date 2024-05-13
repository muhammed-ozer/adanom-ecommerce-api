namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProduct_ProductSpecificationAttributeValidator : AbstractValidator<DeleteProduct_ProductSpecificationAttribute>
    {
        private readonly IMediator _mediator;

        public DeleteProduct_ProductSpecificationAttributeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .MustAsync(ValidateDoesProduct_ProductSpecificationAttributeExistsAsync)
                    .WithMessage("Ürün ve ürün özelliği ilişkisi bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesProduct_ProductSpecificationAttributeExistsAsync

        private async Task<bool> ValidateDoesProduct_ProductSpecificationAttributeExistsAsync(
            DeleteProduct_ProductSpecificationAttribute value,
            CancellationToken cancellationToken)
        {
            var product_ProductSpecificationAttributeExists = await _mediator.Send(
                new DoesProduct_ProductSpecificationAttributeExists(value.ProductId, value.ProductSpecificationAttributeId));

            if (!product_ProductSpecificationAttributeExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}
