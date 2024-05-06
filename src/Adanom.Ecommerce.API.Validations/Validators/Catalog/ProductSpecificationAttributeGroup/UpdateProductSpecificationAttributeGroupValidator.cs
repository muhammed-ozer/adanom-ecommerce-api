namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductSpecificationAttributeGroupValidator : AbstractValidator<UpdateProductSpecificationAttributeGroup>
    {
        private readonly IMediator _mediator;

        public UpdateProductSpecificationAttributeGroupValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün özellik grubu bulunamadı.")
                .CustomAsync(ValidateDoesProductSpecificationAttributeGroupExistsAsync);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Ürün özeelik grubu adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Ürün özeelik grubu adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e)
                .MustAsync(ValidateDoesProductSpecificationAttributeGroupNameNotExistsAsync)
                    .WithMessage("Bu isimde başka bir ürün özeelik grubu bulunmaktadır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesProductSpecificationAttributeGroupExistsAsync

        private async Task ValidateDoesProductSpecificationAttributeGroupExistsAsync(
            long value,
            ValidationContext<UpdateProductSpecificationAttributeGroup> context,
            CancellationToken cancellationToken)
        {
            var productSpecificationAttributeGroupExists = await _mediator.Send(new DoesEntityExists<ProductSpecificationAttributeGroupResponse>(value));

            if (!productSpecificationAttributeGroupExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductSpecificationAttributeGroup.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün özellik grubu bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductSpecificationAttributeGroupNameNotExistsAsync

        private async Task<bool> ValidateDoesProductSpecificationAttributeGroupNameNotExistsAsync(
            UpdateProductSpecificationAttributeGroup value,
            CancellationToken cancellationToken)
        {
            var productSpecificationAttributeGroupNameExists = await _mediator.Send(
                new DoesEntityNameExists<ProductSpecificationAttributeGroupResponse>(value.Name, value.Id));

            if (productSpecificationAttributeGroupNameExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}
