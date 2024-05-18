namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeletePickUpStoreValidator : AbstractValidator<DeletePickUpStore>
    {
        private readonly IMediator _mediator;

        public DeletePickUpStoreValidator(IMediator mediator)
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
        }

        #region Private Methods

        #region ValidateDoesPickUpStoreExistsAsync

        private async Task ValidateDoesPickUpStoreExistsAsync(
            long value,
            ValidationContext<DeletePickUpStore> context,
            CancellationToken cancellationToken)
        {
            var pickuUpStoreExists = await _mediator.Send(new DoesEntityExists<PickUpStoreResponse>(value));

            if (!pickuUpStoreExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeletePickUpStore.Id), null)
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
