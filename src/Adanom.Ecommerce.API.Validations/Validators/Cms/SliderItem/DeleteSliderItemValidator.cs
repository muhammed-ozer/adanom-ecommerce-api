namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteSliderItemValidator : AbstractValidator<DeleteSliderItem>
    {
        private readonly IMediator _mediator;

        public DeleteSliderItemValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Slayt bulunamadı.")
                .CustomAsync(ValidateDoesSliderItemExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesSliderItemExistsAsync

        private async Task ValidateDoesSliderItemExistsAsync(
            long value,
            ValidationContext<DeleteSliderItem> context,
            CancellationToken cancellationToken)
        {
            var sliderItemExists = await _mediator.Send(new DoesEntityExists<SliderItemResponse>(value));

            if (!sliderItemExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteSliderItem.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Slayt bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
