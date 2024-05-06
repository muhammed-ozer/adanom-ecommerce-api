namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductTagValidator : AbstractValidator<UpdateProductTag>
    {
        private readonly IMediator _mediator;

        public UpdateProductTagValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün etiketi bulunamadı.")
                .CustomAsync(ValidateDoesProductTagExistsAsync);

            RuleFor(e => e.Value)
                .NotEmpty()
                    .WithMessage("Ürün etiketi değeri gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(100)
                    .WithMessage("Ürün etiketi değeri 100 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesProductTagExistsAsync

        private async Task ValidateDoesProductTagExistsAsync(
            long value,
            ValidationContext<UpdateProductTag> context,
            CancellationToken cancellationToken)
        {
            var productTagExists = await _mediator.Send(new DoesEntityExists<ProductTagResponse>(value));

            if (!productTagExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductTag.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün etiketi bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
