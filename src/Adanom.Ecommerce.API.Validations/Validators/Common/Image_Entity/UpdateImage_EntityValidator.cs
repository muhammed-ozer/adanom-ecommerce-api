namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateImage_EntityValidator : AbstractValidator<UpdateImage_Entity>
    {
        private readonly IMediator _mediator;

        public UpdateImage_EntityValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.ImageId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Görsel bulunamadı.")
                .CustomAsync(ValidateDoesImageExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesImageExistsAsync

        private async Task ValidateDoesImageExistsAsync(
            long value,
            ValidationContext<UpdateImage_Entity> context,
            CancellationToken cancellationToken)
        {
            var imageExists = await _mediator.Send(new DoesEntityExists<ImageResponse>(value));

            if (!imageExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateImage_Entity.ImageId), null)
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
