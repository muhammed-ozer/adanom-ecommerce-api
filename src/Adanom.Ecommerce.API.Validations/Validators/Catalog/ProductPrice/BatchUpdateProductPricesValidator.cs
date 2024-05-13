namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class BatchUpdateProductPricesValidator : AbstractValidator<BatchUpdateProductPrices>
    {
        private readonly IMediator _mediator;

        public BatchUpdateProductPricesValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.File)
                .NotNull()
                    .WithMessage("Ürün fiyatlarını güncellemek için gerekli dosya bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.NULL)
                .Custom(ValidateFileExtensionAllowed);
        }

        #region Private Methods

        #region ValidateFileExtensionAllowed

        private void ValidateFileExtensionAllowed(
            UploadedFile value,
            ValidationContext<BatchUpdateProductPrices> context)
        {
            if (!FileConstants.AllowedExcelExtensions.Contains(value.Extension))
            {
                context.AddFailure(new ValidationFailure(nameof(BatchUpdateProductPrices.File), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ürün fiyatlarını güncellemek için yüklenen dosya .xls/.xlsl uzantısına sahip olmalıdır."
                });
            }
        }

        #endregion

        #endregion
    }
}
