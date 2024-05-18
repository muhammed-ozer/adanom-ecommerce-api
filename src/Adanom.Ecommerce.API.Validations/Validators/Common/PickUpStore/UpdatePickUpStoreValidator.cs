using System.Text.RegularExpressions;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdatePickUpStoreValidator : AbstractValidator<UpdatePickUpStore>
    {
        private readonly IMediator _mediator;

        public UpdatePickUpStoreValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Teslimat mağazası bulunamadı.")
                .CustomAsync(ValidateDoesPickUpStoreExistsAsync);

            RuleFor(e => e.DisplayName)
                .NotEmpty()
                    .WithMessage("Teslimat mağazası görüntüleme adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Teslimat mağazası görüntüleme adı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.Address)
                .NotEmpty()
                    .WithMessage("Teslimat mağazası adresi gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(500)
                    .WithMessage("Teslimat mağazası adresi 500 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                    .WithMessage(e => "Teslimat mağazası telefon numarası gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .Matches(new Regex(@"^\d{10}"))
                    .WithMessage(e => "Teslimat mağazası telefon numarası 10 karakterden oluşmalıdır Örnek: 5300000000.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);
        }

        #region Private Methods

        #region ValidateDoesPickUpStoreExistsAsync

        private async Task ValidateDoesPickUpStoreExistsAsync(
            long value,
            ValidationContext<UpdatePickUpStore> context,
            CancellationToken cancellationToken)
        {
            var pickUpStoreExists = await _mediator.Send(new DoesEntityExists<PickUpStoreResponse>(value));

            if (!pickUpStoreExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdatePickUpStore.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Teslimat mağazası bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
