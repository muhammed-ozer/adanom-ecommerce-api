namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateMetaInformationValidator : AbstractValidator<CreateMetaInformation>
    {
        public CreateMetaInformationValidator()
        {
            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

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
    }
}
