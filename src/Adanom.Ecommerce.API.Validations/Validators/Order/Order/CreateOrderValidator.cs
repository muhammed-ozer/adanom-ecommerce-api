using System.Security.Claims;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateOrderValidator : AbstractValidator<CreateOrder>
    {
        private readonly IMediator _mediator;

        public CreateOrderValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.DistanceSellingContract)
                .Must(ValidateCustomerAggreesDistanceSellingContract)
                    .WithMessage("Mesafeli satış sözleşmesini kabul etmeniz gerekmektedir..")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e.PreliminaryInformationForm)
                .Must(ValidateCustomerAggreesPreliminaryInformationForm)
                    .WithMessage("Ön bilgilendirme formunu kabul etmeniz gerekmektedir..")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e)
                .CustomAsync(ValidateDoesShippingAddressExistsAsync)
                .CustomAsync(ValidateDoesBillingAddressExistsAsync)
                .CustomAsync(ValidateDeliveryTypeAsync)
                .CustomAsync(ValidateOrderPaymentTypeAsync);

            RuleFor(e => e.Note)
                .MaximumLength(100)
                    .WithMessage("Sipariş notu 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesShippingAddressExistsAsync

        private async Task ValidateDoesShippingAddressExistsAsync(
            CreateOrder value,
            ValidationContext<CreateOrder> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var shippingAddressExists = await _mediator.Send(new DoesUserEntityExists<ShippingAddressResponse>(userId, value.ShippingAddressId));

            if (!shippingAddressExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateOrder.ShippingAddressId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Teslimat adresi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesBillingAddressExistsAsync

        private async Task ValidateDoesBillingAddressExistsAsync(
            CreateOrder value,
            ValidationContext<CreateOrder> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            if (value.BillingAddressId != null)
            {
                var billingAddressExists = await _mediator.Send(new DoesUserEntityExists<BillingAddressResponse>(userId, value.BillingAddressId.Value));

                if (!billingAddressExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateOrder.BillingAddressId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Fatura adresi bulunamadı."
                    });
                }
            }
        }

        #endregion

        #region ValidateDeliveryTypeAsync

        private async Task ValidateDeliveryTypeAsync(
            CreateOrder value,
            ValidationContext<CreateOrder> context,
            CancellationToken cancellationToken)
        {
            if (value.DeliveryType == DeliveryType.PICK_UP_FROM_STORE)
            {
                if (value.PickUpStoreId == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateOrder.PickUpStoreId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Teslimat mağazası bulunamadı."
                    });

                    return;
                }

                var pickuUpStoreExists = await _mediator.Send(new DoesEntityExists<PickUpStoreResponse>(value.PickUpStoreId.Value));

                if (!pickuUpStoreExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateOrder.PickUpStoreId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Teslimat mağazası bulunamadı."
                    });

                    return;
                }
            }
            else if (value.DeliveryType == DeliveryType.CARGO_SHIPMENT)
            {
                if (value.ShippingProviderId == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateOrder.ShippingProviderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Kargo firması bulunamadı."
                    });

                    return;
                }

                var shippingProviderExists = await _mediator.Send(new DoesEntityExists<ShippingProviderResponse>(value.ShippingProviderId.Value));

                if (!shippingProviderExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateOrder.ShippingProviderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Kargo firması bulunamadı."
                    });

                    return;
                }
            }
            else if (value.DeliveryType == DeliveryType.LOCAL_DELIVERY)
            {
                if (value.LocalDeliveryProviderId == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateOrder.LocalDeliveryProviderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Yerel teslimat bulunamadı."
                    });

                    return;
                }

                var localDeliveryProviderExists = await _mediator.Send(new DoesEntityExists<LocalDeliveryProviderResponse>(value.LocalDeliveryProviderId.Value));

                if (!localDeliveryProviderExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateOrder.LocalDeliveryProviderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Yerel teslimat bulunamadı."
                    });

                    return;
                }

                var shippingAddress = await _mediator.Send(new GetShippingAddress(value.ShippingAddressId));

                if (shippingAddress == null)
                {
                    return;
                }

                var supportedAddressDistricts = await _mediator.Send(new GetLocalDeliveryProvider_SupportedAddressDistricts(value.LocalDeliveryProviderId.Value));

                if (supportedAddressDistricts == null || !supportedAddressDistricts.Any())
                {
                    return;
                }

                if (!supportedAddressDistricts.Select(e => e.Id).Contains(shippingAddress.AddressDistrictId))
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateOrder.LocalDeliveryProviderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Teslimat adresi için bu teslimat seçeneği kullanılamaz."
                    });

                    return;
                }
            }
            else
            {
                context.AddFailure(new ValidationFailure(null, null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Teslimat yöntemi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateOrderPaymentTypeAsync

        private async Task ValidateOrderPaymentTypeAsync(
           CreateOrder value,
           ValidationContext<CreateOrder> context,
           CancellationToken cancellationToken)
        {
            var allowedPaymentTypes = await _mediator.Send(new GetAllowedPaymentTypesByDeliveryType(value.DeliveryType));

            if (!allowedPaymentTypes.Select(e => e.Key).Contains(value.OrderPaymentType))
            {
                context.AddFailure(new ValidationFailure(nameof(CreateOrder.OrderPaymentType), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ödeme seçeneği bu teslimat yöntemiyle kullanılamaz."
                });

                return;
            }
        }

        #endregion

        #region ValidateCustomerAggreesDistanceSellingContract

        private bool ValidateCustomerAggreesDistanceSellingContract(bool value)
        {
            return value;
        }

        #endregion

        #region ValidateCustomerAggreesPreliminaryInformationForm

        private bool ValidateCustomerAggreesPreliminaryInformationForm(bool value)
        {
            return value;
        }

        #endregion

        #endregion
    }
}
