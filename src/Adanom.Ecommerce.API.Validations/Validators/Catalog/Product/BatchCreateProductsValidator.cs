namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class BatchCreateProductsValidator : AbstractValidator<BatchCreateProducts>
    {
        private readonly IMediator _mediator;

        public BatchCreateProductsValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.File)
                .NotNull()
                    .WithMessage("Toplu ürün oluşturmak için gerekli dosya bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.NULL)
                .Custom(ValidateFileExtensionAllowed);
        }

        #region Private Methods

        #region ValidateFileExtensionAllowed

        private void ValidateFileExtensionAllowed(
            UploadedFile value,
            ValidationContext<BatchCreateProducts> context)
        {
            if (!FileConstants.AllowedExcelExtensions.Contains(value.Extension))
            {
                context.AddFailure(new ValidationFailure(nameof(BatchCreateProducts.File), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Toplu ürün oluşturmak için yüklenen dosya .xls/.xlsl uzantısına sahip olmalıdır."
                });
            }
        }

        #endregion

        #endregion
    }
}
