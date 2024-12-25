using System.Security.Claims;
using System.Text.RegularExpressions;
using HotChocolate;
using HotChocolate.Execution;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateShippingAddressValidator : AbstractValidator<CreateShippingAddress>
    {
        private readonly IMediator _mediator;

        public CreateShippingAddressValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
               .CustomAsync(ValidateDoesUserShippingAddressesCountLessThanLimitAsync);

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
                    .WithMessage("Adres başlığı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(50)
                    .WithMessage("Adres başlığı 50 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e)
               .CustomAsync(ValidateDoesShippingAddressTitleNotExistsAsync);

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

        #region ValidateDoesShippingAddressTitleNotExistsAsync

        private async Task ValidateDoesShippingAddressTitleNotExistsAsync(
            CreateShippingAddress value,
            ValidationContext<CreateShippingAddress> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var titleExists = await _mediator.Send(new DoesUserEntityNameExists<ShippingAddressResponse>(userId, value.Title));

            if (titleExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateShippingAddress.Title), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu adres başlığı kullanımda."
                });
            }
        }

        #endregion

        #region ValidateDoesUserShippingAddressesCountLessThanLimitAsync

        private async Task ValidateDoesUserShippingAddressesCountLessThanLimitAsync(
            CreateShippingAddress value,
            ValidationContext<CreateShippingAddress> context,
            CancellationToken cancellationToken)
        {
            var totalCount = await _mediator.Send(new GetShippingAddressesCount(value.Identity));

            if (totalCount >= 10)
            {
                var error = new Error("En fazla 10 teslimat adresi oluşturabilirsiniz.", ValidationErrorCodesEnum.NOT_ALLOWED.ToString());

                throw new QueryException(error);
            }
        }

        #endregion

        #region ValidateDoesAddressCityExistsAsync

        private async Task ValidateDoesAddressCityExistsAsync(
            long value,
            ValidationContext<CreateShippingAddress> context,
            CancellationToken cancellationToken)
        {
            var addressCityExists = await _mediator.Send(new DoesEntityExists<AddressCityResponse>(value));

            if (!addressCityExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateShippingAddress.AddressCityId), null)
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
            ValidationContext<CreateShippingAddress> context,
            CancellationToken cancellationToken)
        {
            var addressDistrictExists = await _mediator.Send(new DoesEntityExists<AddressDistrictResponse>(value));

            if (!addressDistrictExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateShippingAddress.AddressDistrictId), null)
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
