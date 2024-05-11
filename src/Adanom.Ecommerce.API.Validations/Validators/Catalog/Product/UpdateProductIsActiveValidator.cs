namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductIsActiveValidator : AbstractValidator<UpdateProductIsActive>
    {
        private readonly IMediator _mediator;

        public UpdateProductIsActiveValidator(IMediator mediator)
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
        }

        #region Private Methods

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<UpdateProductIsActive> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductIsActive.Id), null)
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
