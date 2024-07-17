namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateAnonymousShoppingCartItemValidator : AbstractValidator<CreateAnonymousShoppingCartItem>
    {
        private readonly IMediator _mediator;

        public CreateAnonymousShoppingCartItemValidator(IMediator mediator)
        {
            _mediator = mediator;

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

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<CreateAnonymousShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateAnonymousShoppingCartItem.ProductId), null)
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
            ValidationContext<CreateAnonymousShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var isProductActive = await _mediator.Send(new DoesProductIsActive(value));

            if (!isProductActive)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateAnonymousShoppingCartItem.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductActiveAsync

        private async Task ValidateDoesProductHasStocksAsync(
            CreateAnonymousShoppingCartItem value,
            ValidationContext<CreateAnonymousShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var amount = value.Amount;

            if (value.AnonymousShoppingCartId != null)
            {
                var anonymousShoppingCartItem = await _mediator.Send(new GetAnonymousShoppingCartItem(value.AnonymousShoppingCartId.Value, value.ProductId));

                if (anonymousShoppingCartItem != null)
                {
                    amount += anonymousShoppingCartItem.Amount;
                }
            }

            var hasEnoughStocks = await _mediator.Send(new DoesProductHasStocks(value.ProductId, amount));

            if (!hasEnoughStocks)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateAnonymousShoppingCartItem.ProductId), null)
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
