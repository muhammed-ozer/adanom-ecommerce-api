namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteImageValidator : AbstractValidator<DeleteImage>
    {
        private readonly IMediator _mediator;

        public DeleteImageValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Görsel bulunamadı.")
                .CustomAsync(ValidateDoesImageExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesImageExistsAsync

        private async Task ValidateDoesImageExistsAsync(
            long value,
            ValidationContext<DeleteImage> context,
            CancellationToken cancellationToken)
        {
            var imageExists = await _mediator.Send(new DoesEntityExists<ImageResponse>(value));

            if (!imageExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteImage.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Görsel bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
