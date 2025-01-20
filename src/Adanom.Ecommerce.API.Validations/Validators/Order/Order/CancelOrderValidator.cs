namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CancelOrderValidator : AbstractValidator<CancelOrder>
    {
        private readonly IMediator _mediator;

        public CancelOrderValidator(IMediator mediator)
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
                .CustomAsync(ValidateDoesOrderCancelableAsync);
        }

        #region Private Methods

        #region ValidateDoesOrderExistsAsync

        private async Task ValidateDoesOrderExistsAsync(
            long value,
            ValidationContext<CancelOrder> context,
            CancellationToken cancellationToken)
        {
            var orderExists = await _mediator.Send(new DoesEntityExists<OrderResponse>(value));

            if (!orderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CancelOrder.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesOrderCancelableAsync

        private async Task ValidateDoesOrderCancelableAsync(
            CancelOrder value,
            ValidationContext<CancelOrder> context,
            CancellationToken cancellationToken)
        {
            var order = await _mediator.Send(new GetOrder(value.Id));

            if (order == null)
            {
                return;
            }

            var orderStatusType = order.OrderStatusType.Key;

            if (orderStatusType == OrderStatusType.CANCEL)
            {
                context.AddFailure(new ValidationFailure(nameof(CancelOrder.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş zaten iptal edilmiş."
                });

                return;
            }

            if (orderStatusType == OrderStatusType.ON_DELIVERY || orderStatusType == OrderStatusType.DONE)
            {
                context.AddFailure(new ValidationFailure(nameof(CancelOrder.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Teslimatta veya tamamlanmış sipariş iptal edilemez."
                });
            }
        }

        #endregion

        #endregion
    }
}
