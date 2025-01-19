using System.Security.Claims;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class GetCheckoutValidator : AbstractValidator<GetCheckout>
    {
        private readonly IMediator _mediator;

        public GetCheckoutValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .CustomAsync(ValidateDoesShippingAddressExistsAsync)
                .CustomAsync(ValidateDoesBillingAddressExistsAsync)
                .CustomAsync(ValidateDeliveryTypeAsync)
                .CustomAsync(ValidateOrderPaymentTypeAsync);
        }

        #region Private Methods

        #region ValidateDoesShippingAddressExistsAsync

        private async Task ValidateDoesShippingAddressExistsAsync(
            GetCheckout value,
            ValidationContext<GetCheckout> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var shippingAddressExists = await _mediator.Send(new DoesUserEntityExists<ShippingAddressResponse>(userId, value.ShippingAddressId));

            if (!shippingAddressExists)
            {
                context.AddFailure(new ValidationFailure(nameof(GetCheckout.ShippingAddressId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Teslimat adresi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesBillingAddressExistsAsync

        private async Task ValidateDoesBillingAddressExistsAsync(
            GetCheckout value,
            ValidationContext<GetCheckout> context,
            CancellationToken cancellationToken)
        {
            if (value.BillingAddressId != null)
            {
                var userId = value.Identity.GetUserId();

                var billingAddressExists = await _mediator.Send(new DoesUserEntityExists<BillingAddressResponse>(userId, value.BillingAddressId.Value));

                if (!billingAddressExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(GetCheckout.BillingAddressId), null)
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
            GetCheckout value,
            ValidationContext<GetCheckout> context,
            CancellationToken cancellationToken)
        {
            if (value.DeliveryType == DeliveryType.PICK_UP_FROM_STORE)
            {
                if (value.PickUpStoreId == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(GetCheckout.PickUpStoreId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Teslimat noktası bulunamadı."
                    });

                    return;
                }

                var pickuUpStoreExists = await _mediator.Send(new DoesEntityExists<PickUpStoreResponse>(value.PickUpStoreId.Value));

                if (!pickuUpStoreExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(GetCheckout.PickUpStoreId), null)
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
                    var defaultShippingProvider = await _mediator.Send(new GetShippingProvider(true));

                    if (defaultShippingProvider != null)
                    {
                        value.ShippingProviderId = defaultShippingProvider.Id;

                        return;
                    }

                    context.AddFailure(new ValidationFailure(nameof(GetCheckout.ShippingProviderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Kargo firması bulunamadı."
                    });

                    return;
                }

                var shippingProviderExists = await _mediator.Send(new DoesEntityExists<ShippingProviderResponse>(value.ShippingProviderId.Value));

                if (!shippingProviderExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(GetCheckout.ShippingProviderId), null)
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
                    context.AddFailure(new ValidationFailure(nameof(GetCheckout.LocalDeliveryProviderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Yerel teslimat bulunamadı."
                    });

                    return;
                }

                var localDeliveryProviderExists = await _mediator.Send(new DoesEntityExists<LocalDeliveryProviderResponse>(value.LocalDeliveryProviderId.Value));

                if (!localDeliveryProviderExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(GetCheckout.LocalDeliveryProviderId), null)
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
                    context.AddFailure(new ValidationFailure(nameof(GetCheckout.LocalDeliveryProviderId), null)
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
           GetCheckout value,
           ValidationContext<GetCheckout> context,
           CancellationToken cancellationToken)
        {
            var allowedPaymentTypes = await _mediator.Send(new GetAllowedPaymentTypesByDeliveryType(value.DeliveryType));

            if (!allowedPaymentTypes.Select(e => e.Key).Contains(value.OrderPaymentType))
            {
                context.AddFailure(new ValidationFailure(nameof(GetCheckout.OrderPaymentType), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ödeme seçeneği bu teslimat yöntemiyle kullanılamaz."
                });

                return;
            }
        }

        #endregion

        #endregion
    }
}
