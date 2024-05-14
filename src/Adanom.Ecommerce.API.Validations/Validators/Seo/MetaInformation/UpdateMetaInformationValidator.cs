namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateMetaInformationValidator : AbstractValidator<UpdateMetaInformation>
    {
        private readonly IMediator _mediator;

        public UpdateMetaInformationValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("SEO bilgileri bulunamadı.")
                .CustomAsync(ValidateDoesMetaInformationExistsAsync);

            RuleFor(e => e.Title)
                .NotEmpty()
                    .WithMessage("SEO başlığı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("SEO başlığı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Description)
                .NotEmpty()
                    .WithMessage("SEO tanımı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(500)
                    .WithMessage("SEO tanımı 500 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Keywords)
                .NotEmpty()
                    .WithMessage("SEO anahtar kelimeleri gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(1000)
                    .WithMessage("SEO anahtar kelimeleri 1000 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesMetaInformationExistsAsync

        private async Task ValidateDoesMetaInformationExistsAsync(
            long value,
            ValidationContext<UpdateMetaInformation> context,
            CancellationToken cancellationToken)
        {
            var metaInformationExists = await _mediator.Send(new DoesEntityExists<MetaInformationResponse>(value));

            if (!metaInformationExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateMetaInformation.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "SEO bilgileri bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
