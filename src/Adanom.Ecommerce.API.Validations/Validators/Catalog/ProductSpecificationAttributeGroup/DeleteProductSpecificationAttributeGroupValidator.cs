namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProductSpecificationAttributeGroupValidator : AbstractValidator<DeleteProductSpecificationAttributeGroup>
    {
        private readonly IMediator _mediator;

        public DeleteProductSpecificationAttributeGroupValidator(IMediator mediator)
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
                .CustomAsync(ValidateDoesProductSpecificationAttributeGroupExistsAsync)
                .CustomAsync(ValidateDoesNotProductSpecificationAttributeGroupInUseAsync);
        }

        #region Private Methods

        #region ValidateDoesProductSpecificationAttributeGroupExistsAsync

        private async Task ValidateDoesProductSpecificationAttributeGroupExistsAsync(
            long value,
            ValidationContext<DeleteProductSpecificationAttributeGroup> context,
            CancellationToken cancellationToken)
        {
            var productSpecificationAttributeGroupExists = await _mediator.Send(new DoesEntityExists<ProductSpecificationAttributeGroupResponse>(value));

            if (!productSpecificationAttributeGroupExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductSpecificationAttributeGroup.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün özellik grubu bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesNotProductSpecificationAttributeGroupInUseAsync

        private async Task ValidateDoesNotProductSpecificationAttributeGroupInUseAsync(
            long value,
            ValidationContext<DeleteProductSpecificationAttributeGroup> context,
            CancellationToken cancellationToken)
        {
            var productSpecificationAttributeGroupInUse = await _mediator.Send(new DoesEntityInUse<ProductSpecificationAttributeGroupResponse>(value));

            if (productSpecificationAttributeGroupInUse)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductSpecificationAttributeGroup.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu ürün özellik grubu altında ürün özellikleri olduğundan dolayı silinemez."
                });
            }
        }

        #endregion

        #endregion
    }
}
