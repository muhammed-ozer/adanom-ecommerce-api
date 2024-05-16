namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateSliderItemValidator : AbstractValidator<CreateSliderItem>
    {
        public CreateSliderItemValidator()
        {
            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Slayt adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(200)
                    .WithMessage("Slayt 50 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Url)
                .MaximumLength(200)
                    .WithMessage("Slayt yönlendirme linki 1000 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #endregion
    }
}
