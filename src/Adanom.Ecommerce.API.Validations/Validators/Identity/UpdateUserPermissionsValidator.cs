namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateUserPermissionsValidator : AbstractValidator<UpdateUserPermissions>
    {
        public UpdateUserPermissionsValidator()
        {
            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");
        }

        #region Private Methods

        #endregion
    }
}
