namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProductTagValidator : AbstractValidator<CreateProductTag>
    {
        public CreateProductTagValidator()
        {
            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Value)
                .NotEmpty()
                    .WithMessage("Ürün etiketi değeri gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Ürün etiketi değeri 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }
    }
}
