namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateLocalDeliveryProviderValidator : AbstractValidator<CreateLocalDeliveryProvider>
    {
        private readonly IMediator _mediator;

        public CreateLocalDeliveryProviderValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.DisplayName)
                .NotEmpty()
                    .WithMessage("Yerel teslimat sağlayıcı görüntüleme adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Yerel teslimat sağlayıcı görüntüleme adı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Description)
                .MaximumLength(1000)
                    .WithMessage("Yerel teslimat sağlayıcı açıklaması 1000 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.SupportedAddressDistrictIds)
                .CustomAsync(ValidateDoesSupportedAddressDistrictsExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesSupportedAddressDistrictsExistsAsync

        private async Task ValidateDoesSupportedAddressDistrictsExistsAsync(
            ICollection<long> value,
            ValidationContext<CreateLocalDeliveryProvider> context,
            CancellationToken cancellationToken)
        {
            if (!value.Any()) 
            {
                context.AddFailure(new ValidationFailure(nameof(CreateLocalDeliveryProvider.SupportedAddressDistrictIds), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Yerel teslimat için ilçe seçmeniz gerekmektedir."
                });

                return;
            }

            foreach (var addressDistrictId in value) 
            { 
                var addressDistrictExists = await _mediator.Send(new DoesEntityExists<AddressDistrictResponse>(addressDistrictId));

                if (!addressDistrictExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateLocalDeliveryProvider.SupportedAddressDistrictIds), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                        ErrorMessage = "Yerel teslimat için ilçe bulunamadı."
                    });

                    return;
                }
            }
        }

        #endregion

        #endregion
    }
}
