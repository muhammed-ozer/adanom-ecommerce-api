namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateShippingProviderLogoValidator : AbstractValidator<UpdateShippingProviderLogo>
    {
        private readonly IMediator _mediator;

        public UpdateShippingProviderLogoValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kargo firması bulunamadı.")
                .CustomAsync(ValidateDoesShippingProviderLogoExistsAsync);

            RuleFor(e => e.File)
                .NotNull()
                    .WithMessage(e => "Logo dosyası bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.NULL)
                .Custom(ValidateDoesImageExtensionAllowed);
        }

        #region Private Methods

        #region ValidateDoesShippingProviderLogoExistsAsync

        private async Task ValidateDoesShippingProviderLogoExistsAsync(
            long value,
            ValidationContext<UpdateShippingProviderLogo> context,
            CancellationToken cancellationToken)
        {
            var shippingProviderExists = await _mediator.Send(new DoesEntityExists<ShippingProviderResponse>(value));

            if (!shippingProviderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShippingProviderLogo.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Kargo firması bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesImageExtensionAllowed

        private void ValidateDoesImageExtensionAllowed(
            UploadedFile value,
            ValidationContext<UpdateShippingProviderLogo> context)
        {
            if (!FileConstants.AllowedImageExtensions.Contains(value.Extension))
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShippingProviderLogo.File), null)
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
