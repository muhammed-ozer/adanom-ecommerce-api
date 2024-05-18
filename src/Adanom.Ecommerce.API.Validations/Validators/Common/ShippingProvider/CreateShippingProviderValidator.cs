namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateShippingProviderValidator : AbstractValidator<CreateShippingProvider>
    {
        private readonly IMediator _mediator;

        public CreateShippingProviderValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.DisplayName)
                .NotEmpty()
                    .WithMessage("Kargo firması görüntüleme adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Kargo firması görüntüleme adı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #endregion
    }
}
