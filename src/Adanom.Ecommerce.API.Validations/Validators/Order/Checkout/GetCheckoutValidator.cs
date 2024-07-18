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
                .CustomAsync(ValidateDeliveryTypeAsync);
        }

        #region Private Methods

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

        #endregion
    }
}
