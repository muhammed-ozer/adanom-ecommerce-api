namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteLocalDeliveryProviderValidator : AbstractValidator<DeleteLocalDeliveryProvider>
    {
        private readonly IMediator _mediator;

        public DeleteLocalDeliveryProviderValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Yerel teslimat sağlayıcısı bulunamadı.")
                .CustomAsync(ValidateDoesLocalDeliveryProviderExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesLocalDeliveryProviderExistsAsync

        private async Task ValidateDoesLocalDeliveryProviderExistsAsync(
            long value,
            ValidationContext<DeleteLocalDeliveryProvider> context,
            CancellationToken cancellationToken)
        {
            var localDeliveryProviderExists = await _mediator.Send(new DoesEntityExists<LocalDeliveryProviderResponse>(value));

            if (!localDeliveryProviderExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteLocalDeliveryProvider.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Yerel teslimat sağlayıcısı bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
