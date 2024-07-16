using System.Security.Claims;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateShoppingCartItemValidator : AbstractValidator<CreateShoppingCartItem>
    {
        private readonly IMediator _mediator;

        public CreateShoppingCartItemValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

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
            ValidationContext<CreateShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateShoppingCartItem.ProductId), null)
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
            ValidationContext<CreateShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var isProductActive = await _mediator.Send(new DoesProductIsActive(value));

            if (!isProductActive)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateShoppingCartItem.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductActiveAsync

        private async Task ValidateDoesProductHasStocksAsync(
            CreateShoppingCartItem value,
            ValidationContext<CreateShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var shoppingCartItem = await _mediator.Send(new GetShoppingCartItem(userId, value.ProductId));
            var amount = value.Amount;

            if (shoppingCartItem != null)
            {
                amount += shoppingCartItem.Amount;
            }

            var hasEnoughStocks = await _mediator.Send(new DoesProductHasStocks(value.ProductId, amount));

            if (!hasEnoughStocks)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateShoppingCartItem.ProductId), null)
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
