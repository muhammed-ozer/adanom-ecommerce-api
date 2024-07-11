using System.Text.RegularExpressions;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateCompanyValidator : AbstractValidator<UpdateCompany>
    {
        private readonly IMediator _mediator;

        public UpdateCompanyValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.AddressCityId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Şehir bulunamadı.")
                .CustomAsync(ValidateDoesAddressCityExistsAsync);

            RuleFor(e => e.AddressDistrictId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("İlçe bulunamadı.")
                .CustomAsync(ValidateDoesAddressDistrictExistsAsync);

            RuleFor(e => e.LegalName)
                .NotEmpty()
                    .WithMessage("Şirket resmi adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Şirket resmi adı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.DisplayName)
                .NotEmpty()
                    .WithMessage("Şirket görüntüleme adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Şirket görüntüleme adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Address)
                .NotEmpty()
                    .WithMessage("Şirket adresi gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(500)
                    .WithMessage("Şirket adresi 500 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Email)
                .NotEmpty()
                    .WithMessage("Şirket e-posta adresi gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Şirket e-posta adresi 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                    .WithMessage(e => "Şirket telefon numarası gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Matches(new Regex(@"^\d{10}"))
                    .WithMessage(e => "Şirket telefon numarası 10 karakterden oluşmalıdır Örnek: 5300000000.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.TaxAdministration)
                .NotEmpty()
                    .WithMessage("Şirket vergi dairesi adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Şirket vergi dairesi adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.TaxNumber)
                .NotEmpty()
                    .WithMessage(e => "Şirket vergi numarası gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Matches(new Regex(@"^\d{10}"))
                    .WithMessage(e => "Şirket vergi numarası 10 karakterden oluşmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.MersisNumber)
                .NotEmpty()
                    .WithMessage(e => "Şirket MERSİS numarası gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(50)
                    .WithMessage(e => "Şirket MERSİS numarası 50 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesAddressCityExistsAsync

        private async Task ValidateDoesAddressCityExistsAsync(
            long value,
            ValidationContext<UpdateCompany> context,
            CancellationToken cancellationToken)
        {
            var addressCityExists = await _mediator.Send(new DoesEntityExists<AddressCityResponse>(value));

            if (!addressCityExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateCompany.AddressCityId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Şehir bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesAddressDistrictExistsAsync

        private async Task ValidateDoesAddressDistrictExistsAsync(
            long value,
            ValidationContext<UpdateCompany> context,
            CancellationToken cancellationToken)
        {
            var addressDistrictExists = await _mediator.Send(new DoesEntityExists<AddressDistrictResponse>(value));

            if (!addressDistrictExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateCompany.AddressDistrictId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "İlçe bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
