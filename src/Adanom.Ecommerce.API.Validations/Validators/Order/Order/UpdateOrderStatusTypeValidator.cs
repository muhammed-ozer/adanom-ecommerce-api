using FluentValidation;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateOrderStatusTypeValidator : AbstractValidator<UpdateOrder_OrderStatusType>
    {
        private readonly IMediator _mediator;

        public UpdateOrderStatusTypeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Sipariş bulunamadı.")
                .CustomAsync(ValidateDoesOrderExistsAsync);

            RuleFor(e => e)
                .Must(ValidateShippingTrackingCodeNotNullOrEmptyWhenOrderStatusTypeEqualsDeliveredToShippingProvider)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Sipariş durumu kargoya teslim edildi olarak düzenlenecekse kargo kodu gereklidir.");
        }

        #region Private Methods

        #region ValidateDoesOrderExistsAsync

        private async Task ValidateDoesOrderExistsAsync(
            long value,
            ValidationContext<UpdateOrder_OrderStatusType> context,
            CancellationToken cancellationToken)
        {
            var orderExists = await _mediator.Send(new DoesEntityExists<OrderResponse>(value));

            if (!orderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrder_OrderStatusType.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateShippingTrackingCodeNotNullOrEmptyWhenOrderStatusTypeEqualsDeliveredToShippingProvider

        private bool ValidateShippingTrackingCodeNotNullOrEmptyWhenOrderStatusTypeEqualsDeliveredToShippingProvider(UpdateOrder_OrderStatusType value)
        {
            if (value.NewOrderStatusType == OrderStatusType.DELIVERED_TO_SHIPPING_PROVIDER)
            {
                if (string.IsNullOrEmpty(value.ShippingTrackingCode))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #endregion
    }
}
