using System.Security.Claims;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateStockNotificationItemValidator : AbstractValidator<CreateStockNotificationItem>
    {
        private readonly IMediator _mediator;

        public CreateStockNotificationItemValidator(IMediator mediator)
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
               .CustomAsync(ValidateDoesProductExistsAsync);

            RuleFor(e => e)
               .CustomAsync(CanUserAddProductToStockNotificationItemsAsync);

            // TODO: Implement stock control
        }

        #region Private Methods

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<CreateStockNotificationItem> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateStockNotificationItem.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region CanUserAddProductToStockNotificationItemsAsync

        private async Task CanUserAddProductToStockNotificationItemsAsync(
            CreateStockNotificationItem value,
            ValidationContext<CreateStockNotificationItem> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var canUserAddProductToStockNotificationItems = await _mediator.Send(new CanUserAddProductToStockNotificationItems(userId, value.ProductId));

            if (!canUserAddProductToStockNotificationItems)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateStockNotificationItem.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün stok bildirim listenize eklenmiş durumda."
                });
            }
        }

        #endregion

        #endregion
    }
}
