namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductReviewValidator : AbstractValidator<UpdateProductReview>
    {
        private readonly IMediator _mediator;

        public UpdateProductReviewValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün değerlendirmesi bulunamadı.")
                .CustomAsync(ValidateDoesProductReviewExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesProductReviewExistsAsync

        private async Task ValidateDoesProductReviewExistsAsync(
            long value,
            ValidationContext<UpdateProductReview> context,
            CancellationToken cancellationToken)
        {
            var productReviewExists = await _mediator.Send(new DoesEntityExists<ProductReviewResponse>(value));

            if (!productReviewExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductReview.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün değerlendirmesi bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
