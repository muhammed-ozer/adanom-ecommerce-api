namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateImageValidator : AbstractValidator<CreateImage>
    {
        private readonly IMediator _mediator;

        public CreateImageValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .CustomAsync(ValidateDoesEntityExistsAsync);

            RuleFor(e => e.EntityNameAsUrlSlug)
                .NotEmpty()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Obje adı gereklidir.");

            RuleFor(e => e.File)
                .Custom(ValidateDoesImageExtensionAllowed);
        }

        #region Private Methods

        #region ValidateDoesEntityExistsAsync

        private async Task ValidateDoesEntityExistsAsync(
            CreateImage value,
            ValidationContext<CreateImage> context,
            CancellationToken cancellationToken)
        {
            if (value.EntityId <= 0)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateImage.EntityId), null)
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
                        context.AddFailure(new ValidationFailure(nameof(CreateImage.EntityId), null)
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
                        context.AddFailure(new ValidationFailure(nameof(CreateImage.EntityId), null)
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
                        context.AddFailure(new ValidationFailure(nameof(CreateImage.EntityId), null)
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
