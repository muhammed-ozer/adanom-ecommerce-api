using FluentValidation;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateOrder_OrderStatusTypeValidator : AbstractValidator<UpdateOrder_OrderStatusType>
    {
        private readonly IMediator _mediator;

        public UpdateOrder_OrderStatusTypeValidator(IMediator mediator)
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
                .Must(ValidateShippingTrackingCodeNotNullOrEmptyWhenOrderStatusTypeEqualsOnDelivery)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Sipariş durumu kargoya teslim edildi olarak düzenlenecekse kargo kodu gereklidir.");

            RuleFor(e => e)
                .Must(ValidateDoesOrderStatusTypeNotOnDeliveryWhenDeliveryTypePickUpFromStore)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Sipariş mağazadan teslim alınacağı için teslimatta durumuna getirilemez.");

            RuleFor(e => e)
                .CustomAsync(ValidateDoesOrderPaymentExistsAndHasPaidValueWhenDeliveryTypeEqualsPickUpStoreOrLocalDeliveryAsync)
                .CustomAsync(ValidateDoesOrderPaymentExistsAndHasPaidValueWhenOrderPaymentTypeEqualsBankTransferAsync)
                .CustomAsync(ValidateDoesOrderPaymentExistsAndHasPaidValueWhenOrderPaymentTypeEqualsOnlinePaymentAsync);
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

        #region ValidateShippingTrackingCodeNotNullOrEmptyWhenOrderStatusTypeEqualsOnDelivery

        private bool ValidateShippingTrackingCodeNotNullOrEmptyWhenOrderStatusTypeEqualsOnDelivery(UpdateOrder_OrderStatusType value)
        {
            if (value.NewOrderStatusType == OrderStatusType.ON_DELIVERY && value.DeliveryType == DeliveryType.CARGO_SHIPMENT)
            {
                if (string.IsNullOrEmpty(value.ShippingTrackingCode))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region ValidateDoesOrderStatusTypeNotOnDeliveryWhenDeliveryTypePickUpFromStore

        private bool ValidateDoesOrderStatusTypeNotOnDeliveryWhenDeliveryTypePickUpFromStore(UpdateOrder_OrderStatusType value)
        {
            if (value.NewOrderStatusType == OrderStatusType.ON_DELIVERY && value.DeliveryType == DeliveryType.PICK_UP_FROM_STORE)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region ValidateDoesOrderPaymentExistsAndHasPaidValueWhenDeliveryTypeEqualsPickUpStoreOrLocalDeliveryAsync

        private async Task ValidateDoesOrderPaymentExistsAndHasPaidValueWhenDeliveryTypeEqualsPickUpStoreOrLocalDeliveryAsync(
            UpdateOrder_OrderStatusType value,
            ValidationContext<UpdateOrder_OrderStatusType> context,
            CancellationToken cancellationToken)
        {
            if (value.DeliveryType == DeliveryType.CARGO_SHIPMENT)
            {
                return;
            }

            if (value.NewOrderStatusType != OrderStatusType.DONE)
            {
                return;
            }

            var orderPaymentExists = await _mediator.Send(new DoesOrderPaymentExistsByOrderId(value.Id));

            if (!orderPaymentExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrder_OrderStatusType.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş ödemesi gerçekleşmediği için tamamlanamaz."
                });

                return;
            }

            var orderPaymentPaid = await _mediator.Send(new DoesOrderPaymentPaid(value.Id));

            if (!orderPaymentPaid)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrder_OrderStatusType.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş ödemesi gerçekleşmediği için tamamlanamaz."
                });
            }
        }

        #endregion

        #region ValidateDoesOrderPaymentExistsAndHasPaidValueWhenOrderPaymentTypeEqualsOnlinePaymentAsync

        private async Task ValidateDoesOrderPaymentExistsAndHasPaidValueWhenOrderPaymentTypeEqualsOnlinePaymentAsync(
            UpdateOrder_OrderStatusType value,
            ValidationContext<UpdateOrder_OrderStatusType> context,
            CancellationToken cancellationToken)
        {
            if (value.NewOrderStatusType != OrderStatusType.NEW)
            {
                return;
            }

            if (value.OrderPaymentType != OrderPaymentType.ONLINE_PAYMENT)
            {
                return;
            }

            var orderPaymentExists = await _mediator.Send(new DoesOrderPaymentExistsByOrderId(value.Id));

            if (!orderPaymentExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrder_OrderStatusType.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş ödemesi bulunamadı, lütfen ödeme durumunu kontrol ediniz."
                });

                return;
            }

            var orderPaymentPaid = await _mediator.Send(new DoesOrderPaymentPaid(value.Id));

            if (!orderPaymentPaid)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrder_OrderStatusType.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş ödemesi bulunamadı, lütfen ödeme durumunu kontrol ediniz."
                });
            }
        }

        #endregion

        #region ValidateDoesOrderPaymentExistsAndHasPaidValueWhenOrderPaymentTypeEqualsBankTransferAsync

        private async Task ValidateDoesOrderPaymentExistsAndHasPaidValueWhenOrderPaymentTypeEqualsBankTransferAsync(
            UpdateOrder_OrderStatusType value,
            ValidationContext<UpdateOrder_OrderStatusType> context,
            CancellationToken cancellationToken)
        {
            if (value.OrderPaymentType != OrderPaymentType.BANK_TRANSFER)
            {
                return;
            }

            if (value.NewOrderStatusType == OrderStatusType.NEW)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrder_OrderStatusType.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Banka havalesi ödeme yöntemli sipariş için bu durum kullanılamaz. Onaylandı durumuna geçirebilirsiniz."
                });

                return;
            }

            if (value.NewOrderStatusType != OrderStatusType.APPROVED)
            {
                return;
            }

            var orderPaymentExists = await _mediator.Send(new DoesOrderPaymentExistsByOrderId(value.Id));

            if (!orderPaymentExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrder_OrderStatusType.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş ödemesi bulunamadı, lütfen ödeme durumunu kontrol ediniz."
                });

                return;
            }

            var orderPaymentPaid = await _mediator.Send(new DoesOrderPaymentPaid(value.Id));

            if (!orderPaymentPaid)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateOrder_OrderStatusType.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş ödemesi bulunamadı, lütfen ödeme durumunu kontrol ediniz."
                });
            }
        }

        #endregion

        #endregion
    }
}
