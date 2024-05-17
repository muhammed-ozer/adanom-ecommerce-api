namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteShippingProviderValidator : AbstractValidator<DeleteShippingProvider>
    {
        private readonly IMediator _mediator;

        public DeleteShippingProviderValidator(IMediator mediator)
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
        }

        #region Private Methods

        #region ValidateDoesShippingProviderExistsAsync

        private async Task ValidateDoesShippingProviderExistsAsync(
            long value,
            ValidationContext<DeleteShippingProvider> context,
            CancellationToken cancellationToken)
        {
            var shippingProviderExists = await _mediator.Send(new DoesEntityExists<ShippingProviderResponse>(value));

            if (!shippingProviderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteShippingProvider.Id), null)
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
