namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateOrderPaymentValidator : AbstractValidator<CreateOrderPayment>
    {
        private readonly IMediator _mediator;

        public CreateOrderPaymentValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.OrderId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Sipariş bulunamadı.")
                .CustomAsync(ValidateDoesOrderExistsAsync);

            RuleFor(e => e.TransactionId)
                .NotEmpty()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ödeme işlem numarası gereklidir.")
                        .When(e => e.OrderPaymentType == OrderPaymentType.ONLINE_PAYMENT);

            RuleFor(e => e.GatewayInitializationResponse)
                .NotEmpty()
                .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ödeme işlemi başlatma bilgileri gereklidir.")
                        .When(e => e.OrderPaymentType == OrderPaymentType.ONLINE_PAYMENT);

            RuleFor(e => e)
                .CustomAsync(ValidateOrderPaymentDoesntPaidAsync);
        }

        #region Private Methods

        #region ValidateDoesOrderExistsAsync

        private async Task ValidateDoesOrderExistsAsync(
            long value,
            ValidationContext<CreateOrderPayment> context,
            CancellationToken cancellationToken)
        {
            var orderExists = await _mediator.Send(new DoesEntityExists<OrderResponse>(value));

            if (!orderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateOrderPayment.OrderId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateOrderPaymentDoesntPaidAsync

        private async Task ValidateOrderPaymentDoesntPaidAsync(
            CreateOrderPayment value,
            ValidationContext<CreateOrderPayment> context,
            CancellationToken cancellationToken)
        {
            var orderPaymentPaid = await _mediator.Send(new DoesOrderPaymentPaid(value.OrderId));

            if (orderPaymentPaid)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateOrderPayment.OrderId), null)
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
