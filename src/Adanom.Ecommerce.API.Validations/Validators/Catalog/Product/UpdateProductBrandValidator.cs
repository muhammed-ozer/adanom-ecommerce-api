namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductBrandValidator : AbstractValidator<UpdateProductBrand>
    {
        private readonly IMediator _mediator;

        public UpdateProductBrandValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün bulunamadı.")
                .CustomAsync(ValidateDoesProductExistsAsync);

            RuleFor(e => e.BrandId)
                .CustomAsync(ValidateDoesBrandExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<UpdateProductBrand> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductBrand.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesBrandExistsAsync

        private async Task ValidateDoesBrandExistsAsync(
            long? value,
            ValidationContext<UpdateProductBrand> context,
            CancellationToken cancellationToken)
        {
            if (value != null)
            {
                var brandExists = await _mediator.Send(new DoesEntityExists<BrandResponse>(value.Value));

                if (!brandExists)
                {
                    context.AddFailure(new ValidationFailure(nameof(UpdateProductBrand.Id), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                        ErrorMessage = "Marka bulunamadı."
                    });
                }
            }
        }

        #endregion

        #endregion
    }
}
