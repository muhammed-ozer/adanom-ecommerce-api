namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteProductTagValidator : AbstractValidator<DeleteProductTag>
    {
        private readonly IMediator _mediator;

        public DeleteProductTagValidator(IMediator mediator)
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
        }

        #region Private Methods

        #region ValidateDoesProductTagExistsAsync

        private async Task ValidateDoesProductTagExistsAsync(
            long value,
            ValidationContext<DeleteProductTag> context,
            CancellationToken cancellationToken)
        {
            var productTagExists = await _mediator.Send(new DoesEntityExists<ProductTagResponse>(value));

            if (!productTagExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteProductTag.Id), null)
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
