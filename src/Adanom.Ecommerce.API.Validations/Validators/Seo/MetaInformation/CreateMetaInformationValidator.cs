namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateMetaInformationValidator : AbstractValidator<CreateMetaInformation>
    {
        private readonly IMediator _mediator;

        public CreateMetaInformationValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .CustomAsync(ValidateDoesEntityExistsAsync);

            RuleFor(e => e.Title)
                .NotEmpty()
                    .WithMessage("SEO başlığı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("SEO başlığı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Description)
                .NotEmpty()
                    .WithMessage("SEO tanımı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(500)
                    .WithMessage("SEO tanımı 500 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Keywords)
                .NotEmpty()
                    .WithMessage("SEO anahtar kelimeleri gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(1000)
                    .WithMessage("SEO anahtar kelimeleri 1000 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesEntityExistsAsync

        private async Task ValidateDoesEntityExistsAsync(
            CreateMetaInformation value,
            ValidationContext<CreateMetaInformation> context,
            CancellationToken cancellationToken)
        {
            if (value.EntityId <= 0)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateMetaInformation.EntityId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Obje bulunamadı."
                });
            }

            switch (value.EntityType)
            {
                case EntityType.PRODUCT:
                    var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value.EntityId));

                    if (!productExists)
                    {
                        context.AddFailure(new ValidationFailure(nameof(CreateMetaInformation.EntityId), null)
                        {
                            ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                            ErrorMessage = "Ürün bulunamadı."
                        });
                    }

                    break;
                case EntityType.PRODUCTCATEGORY:

                    var productCategoryExists = await _mediator.Send(new DoesEntityExists<ProductCategoryResponse>(value.EntityId));

                    if (!productCategoryExists)
                    {
                        context.AddFailure(new ValidationFailure(nameof(CreateMetaInformation.EntityId), null)
                        {
                            ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                            ErrorMessage = "Ürün kategorisi bulunamadı."
                        });
                    }

                    break;
                case EntityType.BRAND:

                    var brandExists = await _mediator.Send(new DoesEntityExists<BrandResponse>(value.EntityId));

                    if (!brandExists)
                    {
                        context.AddFailure(new ValidationFailure(nameof(CreateMetaInformation.EntityId), null)
                        {
                            ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                            ErrorMessage = "Marka bulunamadı."
                        });
                    }

                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ValidateDoesImageExtensionAllowed

        private void ValidateDoesImageExtensionAllowed(
            UploadedFile value,
            ValidationContext<CreateImage> context)
        {
            if (!FileConstants.AllowedImageExtensions.Contains(value.Extension))
            {
                context.AddFailure(new ValidationFailure(nameof(CreateImage.File), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Görsel uzantısı JPEG, JPG veya PNG olmalıdır."
                });
            }
        }

        #endregion

        #endregion
    }
}
