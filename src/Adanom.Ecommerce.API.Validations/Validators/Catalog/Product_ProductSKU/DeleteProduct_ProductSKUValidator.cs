namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProduct_ProductSKUValidator : AbstractValidator<DeleteProduct_ProductSKU>
    {
        public DeleteProduct_ProductSKUValidator()
        {
            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");
        }
    }
}
