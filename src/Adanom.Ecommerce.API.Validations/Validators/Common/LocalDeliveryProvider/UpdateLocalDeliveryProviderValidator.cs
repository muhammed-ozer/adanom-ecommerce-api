namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateLocalDeliveryProviderValidator : AbstractValidator<UpdateLocalDeliveryProvider>
    {
        private readonly IMediator _mediator;

        public UpdateLocalDeliveryProviderValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Yerel teslimat sağlayıcısı bulunamadı.")
                .CustomAsync(ValidateDoesLocalDeliveryProviderExistsAsync);

            RuleFor(e => e.DisplayName)
                .NotEmpty()
                    .WithMessage("Yerel teslimat sağlayıcı görüntüleme adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Yerel teslimat sağlayıcı görüntüleme adı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Description)
                .MaximumLength(1000)
                    .WithMessage("Yerel teslimat sağlayıcı açıklaması 1000 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.SupportedAddressDistrictIds)
                .CustomAsync(ValidateDoesSupportedAddressDistrictsExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesLocalDeliveryProviderExistsAsync

        private async Task ValidateDoesLocalDeliveryProviderExistsAsync(
            long value,
            ValidationContext<UpdateLocalDeliveryProvider> context,
            CancellationToken cancellationToken)
        {
            var localDeliveryProviderExists = await _mediator.Send(new DoesEntityExists<LocalDeliveryProviderResponse>(value));

            if (!localDeliveryProviderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateLocalDeliveryProvider.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Yerel teslimat sağlayıcısı bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesSupportedAddressDistrictsExistsAsync

        private async Task ValidateDoesSupportedAddressDistrictsExistsAsync(
            ICollection<long> value,
            ValidationContext<UpdateLocalDeliveryProvider> context,
            CancellationToken cancellationToken)
        {
            if (!value.Any())
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateLocalDeliveryProvider.SupportedAddressDistrictIds), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Yerel teslimat için ilçe seçmeniz gerekmektedir."
                });

                return;
            }

            foreach (var addressDistrictId in value)
            {
                var addressDistrictExists = await _mediator.Send(new DoesEntityExists<AddressDistrictResponse>(addressDistrictId));

                if (!addressDistrictExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(UpdateLocalDeliveryProvider.SupportedAddressDistrictIds), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Yerel teslimat için ilçe bulunamadı."
                    });

                    return;
                }
            }
        }

        #endregion

        #endregion
    }
}
