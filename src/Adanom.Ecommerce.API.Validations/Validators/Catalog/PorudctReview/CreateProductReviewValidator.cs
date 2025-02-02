namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateProductReviewValidator : AbstractValidator<CreateProductReview>
    {
        private readonly IMediator _mediator;

        public CreateProductReviewValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .CustomAsync(ValidateUserCanCreateProductReviewAsync);

            RuleFor(e => e.ProductId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün bulunamadı.")
                .CustomAsync(ValidateDoesProductExistsAsync);


            RuleFor(e => e.Comment)
                .MaximumLength(500)
                    .WithMessage("Ürün yorumu 500 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateUserCanCreateProductReviewAsync

        private async Task ValidateUserCanCreateProductReviewAsync(
            CreateProductReview value,
            ValidationContext<CreateProductReview> context,
            CancellationToken cancellationToken)
        {
            var canCreate = await _mediator.Send(new UserCanCreateProductReview(value.Identity, value.ProductId));

            if (!canCreate)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProductReview.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün değerlendirmesi yapabilmek için ürünü satın almış olmanız gerekmektedir."
                });
            }
        }

        #endregion

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<CreateProductReview> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateProductReview.ProductId), null)
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
