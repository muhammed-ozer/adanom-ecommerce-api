namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class ProceedToCheckoutValidator : AbstractValidator<ProceedToCheckout>
    {
        private readonly IMediator _mediator;

        public ProceedToCheckoutValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .CustomAsync(ValidateDoesShoppingCartItemsHasNotChanges);
        }

        #region Private Methods

        #region ValidateDoesShoppingCartItemsHasNotChanges

        private async Task ValidateDoesShoppingCartItemsHasNotChanges(
            ProceedToCheckout value,
            ValidationContext<ProceedToCheckout> context,
            CancellationToken cancellationToken)
        {
            var updateSHoppingCartItemsResponse = await _mediator.Send(new UpdateShoppingCartItems(value.Identity));

            if (updateSHoppingCartItemsResponse.HasNoItem)
            {
                context.AddFailure(new ValidationFailure(null, null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sepetinizde ürün bulunamadı."
                });
            }

            if (updateSHoppingCartItemsResponse.HasStocksChanges)
            {
                context.AddFailure(new ValidationFailure(null, null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sepetinizdeki ürünlerde stok değişikliği olduğu için sepetinizi kontrol ediniz."
                });
            }

            if (updateSHoppingCartItemsResponse.HasPriceChanges)
            {
                context.AddFailure(new ValidationFailure(null, null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sepetinizdeki ürünlerde fiyat değişikliği olduğu için sepetinizi kontrol ediniz."
                });
            }

            if (updateSHoppingCartItemsResponse.HasProductDeleted)
            {
                context.AddFailure(new ValidationFailure(null, null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sepetinizden silinen ürünler olduğu için sepetinizi kontrol ediniz."
                });
            }
        }

        #endregion

        #endregion
    }
}
