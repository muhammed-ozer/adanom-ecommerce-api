namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProduct_ProductTagValidator : AbstractValidator<DeleteProduct_ProductTag>
    {
        public DeleteProduct_ProductTagValidator()
        {
            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");
        }
    }
}
