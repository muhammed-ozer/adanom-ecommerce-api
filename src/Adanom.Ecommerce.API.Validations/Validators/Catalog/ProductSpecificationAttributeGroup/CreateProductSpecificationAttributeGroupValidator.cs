namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProductSpecificationAttributeGroupValidator : AbstractValidator<CreateProductSpecificationAttributeGroup>
    {
        private readonly IMediator _mediator;

        public CreateProductSpecificationAttributeGroupValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Ürün özellik grubu adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Ürün özellik grubu adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                .CustomAsync(ValidateDoesProductSpecificationAttributeGroupNameNotExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProductSpecificationAttributeGroupNameNotExistsAsync

        private async Task ValidateDoesProductSpecificationAttributeGroupNameNotExistsAsync(
            string value,
            ValidationContext<CreateProductSpecificationAttributeGroup> context,
            CancellationToken cancellationToken)
        {
            var productSpecificationAttributeGroupNameExists = await _mediator.Send(new DoesEntityNameExists<ProductSpecificationAttributeGroupResponse>(value));

            if (productSpecificationAttributeGroupNameExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProductSpecificationAttributeGroup.Name), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu isimde başka bir ürün özellik grubu bulunmaktadır."
                });
            }
        }

        #endregion

        #endregion
    }
}
