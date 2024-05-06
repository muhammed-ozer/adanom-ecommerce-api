namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductSpecificationAttributeValidator : AbstractValidator<UpdateProductSpecificationAttribute>
    {
        private readonly IMediator _mediator;

        public UpdateProductSpecificationAttributeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün özelliği bulunamadı.")
                .CustomAsync(ValidateDoesProductSpecificationAttributeExistsAsync);

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

        #region ValidateDoesProductSpecificationAttributeExistsAsync

        private async Task ValidateDoesProductSpecificationAttributeExistsAsync(
            long value,
            ValidationContext<UpdateProductSpecificationAttribute> context,
            CancellationToken cancellationToken)
        {
            var productSpecificationAttributeExists = await _mediator.Send(new DoesEntityExists<ProductSpecificationAttributeResponse>(value));

            if (!productSpecificationAttributeExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductSpecificationAttribute.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün özelliği bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductSpecificationAttributeGroupExistsAsync

        private async Task ValidateDoesProductSpecificationAttributeGroupExistsAsync(
            long value,
            ValidationContext<UpdateProductSpecificationAttribute> context,
            CancellationToken cancellationToken)
        {
            var productSpecificationAttributeGroupExists = await _mediator.Send(new DoesEntityExists<ProductSpecificationAttributeGroupResponse>(value));

            if (!productSpecificationAttributeGroupExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductSpecificationAttribute.ProductSpecificationAttributeGroupId), null)
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
