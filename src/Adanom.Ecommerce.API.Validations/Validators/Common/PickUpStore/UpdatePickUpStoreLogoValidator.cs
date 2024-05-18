namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdatePickUpStoreLogoValidator : AbstractValidator<UpdatePickUpStoreLogo>
    {
        private readonly IMediator _mediator;

        public UpdatePickUpStoreLogoValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Teslimat mağazası bulunamadı.")
                .CustomAsync(ValidateDoesPickUpStoreExistsAsync);

            RuleFor(e => e.File)
                .NotNull()
                    .WithMessage(e => "Logo dosyası bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.NULL)
                .Custom(ValidateDoesImageExtensionAllowed);
        }

        #region Private Methods

        #region ValidateDoesPickUpStoreExistsAsync

        private async Task ValidateDoesPickUpStoreExistsAsync(
            long value,
            ValidationContext<UpdatePickUpStoreLogo> context,
            CancellationToken cancellationToken)
        {
            var pickUpStoreExists = await _mediator.Send(new DoesEntityExists<PickUpStoreResponse>(value));

            if (!pickUpStoreExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdatePickUpStoreLogo.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Teslimat mağazası bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesImageExtensionAllowed

        private void ValidateDoesImageExtensionAllowed(
            UploadedFile value,
            ValidationContext<UpdatePickUpStoreLogo> context)
        {
            if (!FileConstants.AllowedImageExtensions.Contains(value.Extension))
            {
                context.AddFailure(new ValidationFailure(nameof(UpdatePickUpStoreLogo.File), null)
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
