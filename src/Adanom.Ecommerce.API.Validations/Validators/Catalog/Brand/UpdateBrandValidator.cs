namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateBrandValidator : AbstractValidator<UpdateBrand>
    {
        private readonly IMediator _mediator;

        public UpdateBrandValidator(IMediator mediator)
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
                .CustomAsync(ValidateDoesBrandExistsAsync);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Marka adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Marka adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e)
                .MustAsync(ValidateDoesBrandNameNotExistsAsync)
                    .WithMessage("Bu isimde başka bir marka bulunmaktadır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesBrandExistsAsync

        private async Task ValidateDoesBrandExistsAsync(
            long value,
            ValidationContext<UpdateBrand> context,
            CancellationToken cancellationToken)
        {
            var brandExists = await _mediator.Send(new DoesEntityExists<BrandResponse>(value));

            if (!brandExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateBrand.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Marka bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesBrandNameNotExistsAsync

        private async Task<bool> ValidateDoesBrandNameNotExistsAsync(
            UpdateBrand value,
            CancellationToken cancellationToken)
        {
            var brandNameExists = await _mediator.Send(new DoesEntityNameExists<BrandResponse>(value.Name, value.Id));

            if (brandNameExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}
