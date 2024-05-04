namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateBrandValidator : AbstractValidator<CreateBrand>
    {
        private readonly IMediator _mediator;

        public CreateBrandValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Marka adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Marka adı 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN)
                .CustomAsync(ValidateDoesBrandNameNotExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesBrandNameNotExistsAsync

        private async Task ValidateDoesBrandNameNotExistsAsync(
            string value,
            ValidationContext<CreateBrand> context,
            CancellationToken cancellationToken)
        {
            var brandNameExists = await _mediator.Send(new DoesEntityNameExists<BrandResponse>(value));

            if (brandNameExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateBrand.Name), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Bu isimde başka bir marka bulunmaktadır."
                });
            }
        }

        #endregion

        #endregion
    }
}
