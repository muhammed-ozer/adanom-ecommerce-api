using System.Text.RegularExpressions;
using FluentValidation.Results;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class ResetPasswordValidator : AbstractValidator<ResetPassword>
    {
        private readonly IMediator _mediator;

        public ResetPasswordValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Email)
                .NotEmpty()
                    .WithMessage("E-posta gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .CustomAsync(ValidateDoesUserExistsAsync);

            RuleFor(e => e.Token)
                .NotEmpty()
                    .WithMessage("Token bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED);

            RuleFor(e => e.Password)
                .NotEmpty()
                    .WithMessage(e => "Şifre gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
                    .WithMessage(e => "Şifre en az 8 karakter olmalıdır ve en az bir küçük, en az bir büyük harf ve en az bir rakam içermelidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e.ConfirmPassword)
                .NotEmpty()
                    .WithMessage(e => "Şifre tekrarı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Equal(e => e.Password)
                    .WithMessage(e => "Şifre tekrarı, şifre ile aynı olmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesNotUserExistsAsync

        private async Task ValidateDoesUserExistsAsync(
            string value,
            ValidationContext<ResetPassword> context,
            CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new DoesUserExists(value));

            if (!userExists)
            {
                context.AddFailure(new ValidationFailure(nameof(ResetPassword.Email), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Bu işlem sırasında hata meydana geldi. Hatanın devam etmesi durumunda lütfen iletişime geçiniz."
                });
            }
        }

        #endregion

        #endregion
    }
}
