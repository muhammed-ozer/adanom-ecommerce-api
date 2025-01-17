namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateOrderPaymentValidator : AbstractValidator<UpdateOrderPayment>
    {
        private readonly IMediator _mediator;

        public UpdateOrderPaymentValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Sipariş ödemesi bulunamadı.")
                .CustomAsync(ValidateDoesOrderPaymentExistsAsync);

            RuleFor(e => e.OrderId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Sipariş bulunamadı.")
                .CustomAsync(ValidateDoesOrderExistsAsync);

            RuleFor(e => e)
                .CustomAsync(ValidateOrderPaymentDoesntPaidWhenRequestedAsPaidAsync);
        }

        #region Private Methods

        #region ValidateDoesOrderPaymentExistsAsync

        private async Task ValidateDoesOrderPaymentExistsAsync(
            long value,
            ValidationContext<UpdateOrderPayment> context,
            CancellationToken cancellationToken)
        {
            var orderPaymentExists = await _mediator.Send(new DoesEntityExists<OrderPaymentResponse>(value));

            if (!orderPaymentExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrderPayment.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş ödemesi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesOrderExistsAsync

        private async Task ValidateDoesOrderExistsAsync(
            long value,
            ValidationContext<UpdateOrderPayment> context,
            CancellationToken cancellationToken)
        {
            var orderExists = await _mediator.Send(new DoesEntityExists<OrderResponse>(value));

            if (!orderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrderPayment.OrderId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateOrderPaymentDoesntPaidWhenRequestedAsPaidAsync

        private async Task ValidateOrderPaymentDoesntPaidWhenRequestedAsPaidAsync(
            UpdateOrderPayment value,
            ValidationContext<UpdateOrderPayment> context,
            CancellationToken cancellationToken)
        {
            if (!value.Paid)
            {
                return;
            }

            var orderPaymentPaid = await _mediator.Send(new DoesOrderPaymentPaid(value.OrderId));

            if (orderPaymentPaid)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrderPayment.Paid), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş ödemesi mevcut. Tekrar ödeme ekleyemezsiniz."
                });

                return;
            }
        }

        #endregion

        #endregion
    }
}
