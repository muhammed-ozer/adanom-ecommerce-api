namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteShoppingCartItemValidator : AbstractValidator<DeleteShoppingCartItem>
    {
        private readonly IMediator _mediator;

        public DeleteShoppingCartItemValidator(IMediator mediator)
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
        }

        #region Private Methods

        #region ValidateDoesShoppingCartItemExistsAsync

        private async Task ValidateDoesShoppingCartItemExistsAsync(
            long value,
            ValidationContext<DeleteShoppingCartItem> context,
            CancellationToken cancellationToken)
        {
            var shoppingCartItemExists = await _mediator.Send(new DoesEntityExists<ShoppingCartItemResponse>(value));

            if (!shoppingCartItemExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteShoppingCartItem.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sepet ürünü bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
