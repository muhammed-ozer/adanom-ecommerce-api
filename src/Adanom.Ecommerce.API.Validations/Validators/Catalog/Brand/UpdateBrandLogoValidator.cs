namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateBrandLogoValidator : AbstractValidator<UpdateBrandLogo>
    {
        private readonly IMediator _mediator;

        public UpdateBrandLogoValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Marka bulunamadı.")
                .CustomAsync(ValidateDoesBrandExistsAsync);

            RuleFor(e => e.File)
                .NotNull()
                    .WithMessage(e => "Logo dosyası bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.NULL)
                .Custom(ValidateDoesImageExtensionAllowed);
        }

        #region Private Methods

        #region ValidateDoesBrandExistsAsync

        private async Task ValidateDoesBrandExistsAsync(
            long value,
            ValidationContext<UpdateBrandLogo> context,
            CancellationToken cancellationToken)
        {
            var brandExists = await _mediator.Send(new DoesEntityExists<BrandResponse>(value));

            if (!brandExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateBrandLogo.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Marka bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesImageExtensionAllowed

        private void ValidateDoesImageExtensionAllowed(
            UploadedFile value,
            ValidationContext<UpdateBrandLogo> context)
        {
            if (!FileConstants.AllowedImageExtensions.Contains(value.Extension))
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateBrandLogo.File), null)
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
