namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateShippingProviderValidator : AbstractValidator<UpdateShippingProvider>
    {
        private readonly IMediator _mediator;

        public UpdateShippingProviderValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kargo firması bulunamadı.")
                .CustomAsync(ValidateDoesShippingProviderExistsAsync);

            RuleFor(e => e.DisplayName)
                .NotEmpty()
                    .WithMessage("Kargo firması görüntüleme adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Kargo firması görüntüleme adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesShippingProviderExistsAsync

        private async Task ValidateDoesShippingProviderExistsAsync(
            long value,
            ValidationContext<UpdateShippingProvider> context,
            CancellationToken cancellationToken)
        {
            var shippingProviderExists = await _mediator.Send(new DoesEntityExists<ShippingProviderResponse>(value));

            if (!shippingProviderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShippingProvider.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Kargo firması bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
