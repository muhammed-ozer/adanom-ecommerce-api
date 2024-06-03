namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class MarkAsReadNotificationsValidator : AbstractValidator<MarkAsReadNotifications>
    {
        public MarkAsReadNotificationsValidator()
        {
            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");
        }
    }
}
