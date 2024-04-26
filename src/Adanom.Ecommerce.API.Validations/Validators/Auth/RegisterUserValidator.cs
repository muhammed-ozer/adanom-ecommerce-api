using System.Text.RegularExpressions;
using FluentValidation.Results;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        private readonly IMediator _mediator;

        public RegisterUserValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Email)
                .NotEmpty()
                    .WithMessage("E-posta gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .CustomAsync(ValidateDoesNotUserExistsAsync);

            RuleFor(e => e.Password)
                .NotEmpty()
                    .WithMessage(e => "Şifre gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
                    .WithMessage(e => "Şifre en az 8 karakter olmalıdır ve en az bir küçük, en az bir büyük harf ve en az bir rakam içermelidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED);

            RuleFor(e => e.ConfirmPassword)
                .NotEmpty()
                    .WithMessage(e => "Şifre tekrarı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Equal(e => e.Password)
                    .WithMessage(e => "Şifre tekrarı, şifre ile aynı olmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e.FirstName)
                .NotEmpty()
                    .WithMessage(e => "Adınız gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage(e => "Adınız 100 karakterden uzun olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.LastName)
                .NotEmpty()
                    .WithMessage(e => "Soyadınız gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage(e => "Soyadınız 100 karakterden uzun olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                    .WithMessage(e => "Telefon numaranız gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Matches(new Regex(@"^5+\d{9}"))
                    .WithMessage(e => "Telefon numaranız 10 karakterden oluşmalıdır Örnek: 5300000000.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.AgreesDataProtectionPolicy)
                .Equal(true)
                    .WithMessage(e => "Lütfen 'Aydınlatma Metni' ve 'Üyelik Sözleşmesi' ni onaylayınız.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED);
        }

        #region Private Methods

        #region ValidateDoesNotUserExistsAsync

        private async Task ValidateDoesNotUserExistsAsync(
            string value,
            ValidationContext<RegisterUser> context,
            CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new DoesUserExists(value));

            if (userExists)
            {
                context.AddFailure(new ValidationFailure(nameof(RegisterUser.Email), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Bu e-postaya ait başka bir hesap bulunmaktadır."
                });
            }
        }

        #endregion

        #endregion
    }
}
