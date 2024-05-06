namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProductSpecificationAttributeValidator : AbstractValidator<CreateProductSpecificationAttribute>
    {
        private readonly IMediator _mediator;

        public CreateProductSpecificationAttributeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.ProductSpecificationAttributeGroupId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün özellik grubu bulunamadı.")
                .CustomAsync(ValidateDoesProductSpecificationAttributeGroupExistsAsync);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Ürün özelliği adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Ürün özelliği adı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Value)
                .NotEmpty()
                    .WithMessage("Ürün özelliği değeri gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Ürün özelliği değeri 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesProductSpecificationAttributeGroupExistsAsync

        private async Task ValidateDoesProductSpecificationAttributeGroupExistsAsync(
            long value,
            ValidationContext<CreateProductSpecificationAttribute> context,
            CancellationToken cancellationToken)
        {
            var productSpecificationAttributeGroupExists = await _mediator.Send(new DoesEntityExists<ProductSpecificationAttributeGroupResponse>(value));

            if (!productSpecificationAttributeGroupExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProductSpecificationAttribute.ProductSpecificationAttributeGroupId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün özellik grubu bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
