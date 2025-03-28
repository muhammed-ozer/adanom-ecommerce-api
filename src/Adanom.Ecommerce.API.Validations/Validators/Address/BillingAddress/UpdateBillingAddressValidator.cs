﻿using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateBillingAddressValidator : AbstractValidator<UpdateBillingAddress>
    {
        private readonly IMediator _mediator;

        public UpdateBillingAddressValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
               .CustomAsync(ValidateDoesBillingAddressExistsAsync)
               .CustomAsync(ValidateDoesBillingAddressTitleNotExistsAsync);

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
                    .WithMessage("Fatura adres başlığı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(50)
                    .WithMessage("Fatura adres başlığı 50 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Ünvan gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Ünvan 250 karakterden fazla olmamalıdır.")
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

            RuleFor(e => e.Email)
                .NotEmpty()
                    .WithMessage("E-posta adresi gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("E-posta adresi 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.TaxAdministration)
                .NotEmpty()
                    .WithMessage("Vergi dairesi adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Vergi dairesi adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.TaxNumber)
                .NotEmpty()
                    .WithMessage(e => "Vergi numarası gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Matches(new Regex(@"^\d{10,11}"))
                    .WithMessage(e => "Vergi numarası 10 veya 11 karakterden oluşmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesBillingAddressExistsAsync

        private async Task ValidateDoesBillingAddressExistsAsync(
            UpdateBillingAddress value,
            ValidationContext<UpdateBillingAddress> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var billingAddressExists = await _mediator.Send(new DoesUserEntityExists<BillingAddressResponse>(userId, value.Id));

            if (!billingAddressExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateBillingAddress.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Fatura adresi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesBillingAddressTitleNotExistsAsync

        private async Task ValidateDoesBillingAddressTitleNotExistsAsync(
            UpdateBillingAddress value,
            ValidationContext<UpdateBillingAddress> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var titleExists = await _mediator.Send(new DoesUserEntityNameExists<BillingAddressResponse>(userId, value.Title, value.Id));

            if (titleExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateBillingAddress.Title), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu fatura adres başlığı kullanımda."
                });
            }
        }

        #endregion

        #region ValidateDoesAddressCityExistsAsync

        private async Task ValidateDoesAddressCityExistsAsync(
            long value,
            ValidationContext<UpdateBillingAddress> context,
            CancellationToken cancellationToken)
        {
            var addressCityExists = await _mediator.Send(new DoesEntityExists<AddressCityResponse>(value));

            if (!addressCityExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateBillingAddress.AddressCityId), null)
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
            ValidationContext<UpdateBillingAddress> context,
            CancellationToken cancellationToken)
        {
            var addressDistrictExists = await _mediator.Send(new DoesEntityExists<AddressDistrictResponse>(value));

            if (!addressDistrictExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateBillingAddress.AddressDistrictId), null)
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
