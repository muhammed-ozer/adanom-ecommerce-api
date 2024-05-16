namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateSliderItemValidator : AbstractValidator<UpdateSliderItem>
    {
        private readonly IMediator _mediator;

        public UpdateSliderItemValidator(IMediator mediator)
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

            RuleFor(e => e.Url)
                .MaximumLength(200)
                    .WithMessage("Slayt yönlendirme linki 1000 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesSliderItemExistsAsync

        private async Task ValidateDoesSliderItemExistsAsync(
            long value,
            ValidationContext<UpdateSliderItem> context,
            CancellationToken cancellationToken)
        {
            var sliderItemExists = await _mediator.Send(new DoesEntityExists<SliderItemResponse>(value));

            if (!sliderItemExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateSliderItem.Id), null)
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
