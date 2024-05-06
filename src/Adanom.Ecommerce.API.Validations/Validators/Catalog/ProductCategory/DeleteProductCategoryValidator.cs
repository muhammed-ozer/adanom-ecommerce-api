namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProductCategoryValidator : AbstractValidator<DeleteProductCategory>
    {
        private readonly IMediator _mediator;

        public DeleteProductCategoryValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün kategorisi bulunamadı.")
                .CustomAsync(ValidateDoesProductCategoryExistsAsync)
                .CustomAsync(ValidateDoesNotInUseAsync);
        }

        #region Private Methods

        #region ValidateDoesProductCategoryExistsAsync

        private async Task ValidateDoesProductCategoryExistsAsync(
            long value,
            ValidationContext<DeleteProductCategory> context,
            CancellationToken cancellationToken)
        {
            var productCategoryExists = await _mediator.Send(new DoesEntityExists<ProductCategoryResponse>(value));

            if (!productCategoryExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductCategory.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün kategorisi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesNotInUseAsync

        private async Task ValidateDoesNotInUseAsync(
            long value,
            ValidationContext<DeleteProductCategory> context,
            CancellationToken cancellationToken)
        {
            var productCategoryInUse = await _mediator.Send(new DoesEntityInUse<ProductCategoryResponse>(value));

            if (productCategoryInUse)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductCategory.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Ürün kategorisi altında kategoriler veya ürünler olduğundan dolayı silinemez."
                });
            }
        }

        #endregion

        #endregion
    }
}
