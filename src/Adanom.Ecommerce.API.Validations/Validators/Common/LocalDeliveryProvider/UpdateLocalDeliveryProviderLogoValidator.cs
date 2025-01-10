namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateLocalDeliveryProviderLogoValidator : AbstractValidator<UpdateLocalDeliveryProviderLogo>
    {
        private readonly IMediator _mediator;

        public UpdateLocalDeliveryProviderLogoValidator(IMediator mediator)
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

            RuleFor(e => e.File)
                .NotNull()
                    .WithMessage(e => "Logo dosyası bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.NULL)
                .Custom(ValidateDoesImageExtensionAllowed);
        }

        #region Private Methods

        #region ValidateDoesLocalDeliveryProviderExistsAsync

        private async Task ValidateDoesLocalDeliveryProviderExistsAsync(
            long value,
            ValidationContext<UpdateLocalDeliveryProviderLogo> context,
            CancellationToken cancellationToken)
        {
            var localDeliveryProviderExists = await _mediator.Send(new DoesEntityExists<LocalDeliveryProviderResponse>(value));

            if (!localDeliveryProviderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateLocalDeliveryProviderLogo.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Yerel teslimat sağlayıcısı bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesImageExtensionAllowed

        private void ValidateDoesImageExtensionAllowed(
            UploadedFile value,
            ValidationContext<UpdateLocalDeliveryProviderLogo> context)
        {
            if (!FileConstants.AllowedImageExtensions.Contains(value.Extension))
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateLocalDeliveryProviderLogo.File), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Logo dosyası uzantısı jpeg, jpg veya png olmalıdır."
                });
            }
        }

        #endregion

        #endregion
    }
}
