namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class BatchUpdateProductSKUStocksValidator : AbstractValidator<BatchUpdateProductSKUStocks>
    {
        private readonly IMediator _mediator;

        public BatchUpdateProductSKUStocksValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.File)
                .NotNull()
                    .WithMessage("Ürün stokları güncellemek için gerekli dosya bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.NULL)
                .Custom(ValidateFileExtensionAllowed);
        }

        #region Private Methods

        #region ValidateFileExtensionAllowed

        private void ValidateFileExtensionAllowed(
            UploadedFile value,
            ValidationContext<BatchUpdateProductSKUStocks> context)
        {
            if (!FileConstants.AllowedExcelExtensions.Contains(value.Extension))
            {
                context.AddFailure(new ValidationFailure(nameof(BatchUpdateProductSKUStocks.File), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ürün stoklarını güncellemek için yüklenen dosya .xls/.xlsl uzantısına sahip olmalıdır."
                });
            }
        }

        #endregion

        #endregion
    }
}
