using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateShippingAddressValidator : AbstractValidator<UpdateShippingAddress>
    {
        private readonly IMediator _mediator;

        public UpdateShippingAddressValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
               .CustomAsync(ValidateDoesShippingAddressExistsAsync)
               .CustomAsync(ValidateDoesShippingAddressTitleNotExistsAsync);

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

            RuleFor(e => e.Title)
                .NotEmpty()
                    .WithMessage("Başlık gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(50)
                    .WithMessage("Başlık 50 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.FirstName)
                .NotEmpty()
                    .WithMessage("Ad gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Ad 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.LastName)
                .NotEmpty()
                    .WithMessage("Soyad gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Soyad 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Address)
                .NotEmpty()
                    .WithMessage("Adres gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(500)
                    .WithMessage("Adres 500 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.PostalCode)
                .MaximumLength(20)
                    .WithMessage("Posta kodu 20 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                    .WithMessage(e => "Telefon numarası gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Matches(new Regex(@"^\d{10}"))
                    .WithMessage(e => "Telefon numarası 10 karakterden oluşmalıdır Örnek: 5300000000.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesShippingAddressExistsAsync

        private async Task ValidateDoesShippingAddressExistsAsync(
            UpdateShippingAddress value,
            ValidationContext<UpdateShippingAddress> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var shippingAddressExists = await _mediator.Send(new DoesUserEntityExists<ShippingAddressResponse>(userId, value.Id));

            if (!shippingAddressExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShippingAddress.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Teslimat adresi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesShippingAddressTitleNotExistsAsync

        private async Task ValidateDoesShippingAddressTitleNotExistsAsync(
            UpdateShippingAddress value,
            ValidationContext<UpdateShippingAddress> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var titleExists = await _mediator.Send(new DoesUserEntityNameExists<ShippingAddressResponse>(userId, value.Title, value.Id));

            if (titleExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShippingAddress.Title), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu adres başlığı kullanımda."
                });
            }
        }

        #endregion

        #region ValidateDoesAddressCityExistsAsync

        private async Task ValidateDoesAddressCityExistsAsync(
            long value,
            ValidationContext<UpdateShippingAddress> context,
            CancellationToken cancellationToken)
        {
            var addressCityExists = await _mediator.Send(new DoesEntityExists<AddressCityResponse>(value));

            if (!addressCityExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShippingAddress.AddressCityId), null)
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
            ValidationContext<UpdateShippingAddress> context,
            CancellationToken cancellationToken)
        {
            var addressDistrictExists = await _mediator.Send(new DoesEntityExists<AddressDistrictResponse>(value));

            if (!addressDistrictExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateShippingAddress.AddressDistrictId), null)
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
