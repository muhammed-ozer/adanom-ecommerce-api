namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductDisplayOrderValidator : AbstractValidator<UpdateProductDisplayOrder>
    {
        private readonly IMediator _mediator;

        public UpdateProductDisplayOrderValidator(IMediator mediator)
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
            ValidationContext<UpdateProductDisplayOrder> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductDisplayOrder.Id), null)
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
