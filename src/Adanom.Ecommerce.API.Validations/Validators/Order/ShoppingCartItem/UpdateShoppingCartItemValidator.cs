namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateShoppingCartItemValidator : AbstractValidator<UpdateShoppingCartItem>
    {
        private readonly IMediator _mediator;

        public UpdateShoppingCartItemValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Sepet ürünü bulunamadı.")
                .CustomAsync(ValidateDoesShoppingCartItemExistsAsync);

            RuleFor(e => e.ProductId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün bulunamadı.")
                .CustomAsync(ValidateDoesProductExistsAsync)
                .CustomAsync(ValidateDoesProductActiveAsync);

            RuleFor(e => e)
                .CustomAsync(ValidateDoesProductHasStocksAsync);
        }

        #region Private Methods

        #region ValidateDoesShoppingCartItemExistsAsync

        private async Task ValidateDoesShoppingCartItemExistsAsync(
            long value,
            ValidationContext<UpdateShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var shoppingCartItemExists = await _mediator.Send(new DoesEntityExists<ShoppingCartItemResponse>(value));

            if (!shoppingCartItemExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShoppingCartItem.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sepet ürünü bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<UpdateShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShoppingCartItem.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductActiveAsync

        private async Task ValidateDoesProductActiveAsync(
            long value,
            ValidationContext<UpdateShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var isProductActive = await _mediator.Send(new DoesProductIsActive(value));

            if (!isProductActive)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShoppingCartItem.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductActiveAsync

        private async Task ValidateDoesProductHasStocksAsync(
            UpdateShoppingCartItem value,
            ValidationContext<UpdateShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var hasEnoughStocks = await _mediator.Send(new DoesProductHasStocks(value.ProductId, value.Amount));

            if (!hasEnoughStocks)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShoppingCartItem.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürünün yeterli stoğu bulunmamaktadır."
                });
            }
        }

        #endregion

        #endregion
    }
}
