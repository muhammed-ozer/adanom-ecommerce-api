namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteBrandValidator : AbstractValidator<DeleteBrand>
    {
        private readonly IMediator _mediator;

        public DeleteBrandValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Marka bulunamadı.")
                .CustomAsync(ValidateDoesBrandExistsAsync)
                .CustomAsync(ValidateDoesNotBrandInUseAsync);
        }

        #region Private Methods

        #region ValidateDoesBrandExistsAsync

        private async Task ValidateDoesBrandExistsAsync(
            long value,
            ValidationContext<DeleteBrand> context,
            CancellationToken cancellationToken)
        {
            var brandExists = await _mediator.Send(new DoesEntityExists<BrandResponse>(value));

            if (!brandExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteBrand.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Marka bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesNotBrandInUseAsync

        private async Task ValidateDoesNotBrandInUseAsync(
            long value,
            ValidationContext<DeleteBrand> context,
            CancellationToken cancellationToken)
        {
            var brandInUse = await _mediator.Send(new DoesEntityInUse<BrandResponse>(value));

            if (brandInUse)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteBrand.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu marka altında ürünler olduğundan dolayı silinemez."
                });
            }
        }

        #endregion

        #endregion
    }
}
