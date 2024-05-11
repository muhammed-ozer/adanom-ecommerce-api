namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductDescriptionValidator : AbstractValidator<UpdateProductDescription>
    {
        private readonly IMediator _mediator;

        public UpdateProductDescriptionValidator(IMediator mediator)
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

            RuleFor(e => e.Description)
                .MaximumLength(4000)
                    .WithMessage("Ürün açıklaması 4000 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<UpdateProductDescription> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductDescription.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
