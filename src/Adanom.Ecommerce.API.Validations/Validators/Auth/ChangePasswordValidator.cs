using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class ChangePasswordValidator : AbstractValidator<ChangePassword>
    {
        private readonly IMediator _mediator;

        public ChangePasswordValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.")
                    .CustomAsync(ValidateDoesUserExistsAsync);

            RuleFor(e => e.OldPassword)
                .NotEmpty()
                    .WithMessage(e => "Eski şifre gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED);

            RuleFor(e => e.NewPassword)
                .NotEmpty()
                    .WithMessage(e => "Şifre gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Matches(new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
                    .WithMessage(e => "Şifre en az 8 karakter olmalıdır ve en az bir küçük, en az bir büyük harf ve en az bir rakam içermelidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);

            RuleFor(e => e.ConfirmNewPassword)
                .NotEmpty()
                    .WithMessage(e => "Şifre tekrarı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Equal(e => e.NewPassword)
                    .WithMessage(e => "Şifre tekrarı, şifre ile aynı olmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesNotUserExistsAsync

        private async Task ValidateDoesUserExistsAsync(
            ClaimsPrincipal value,
            ValidationContext<ChangePassword> context,
            CancellationToken cancellationToken)
        {
            var userId = value.GetUserId();

            var userExists = await _mediator.Send(new DoesUserExists(userId));

            if (!userExists)
            {
                context.AddFailure(new ValidationFailure(nameof(ChangePassword.Identity), null)
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
