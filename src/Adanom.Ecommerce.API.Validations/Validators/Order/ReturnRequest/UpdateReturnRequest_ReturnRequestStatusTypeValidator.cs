namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateReturnRequest_ReturnRequestStatusTypeValidator : AbstractValidator<UpdateReturnRequest_ReturnRequestStatusType>
    {
        private readonly IMediator _mediator;

        public UpdateReturnRequest_ReturnRequestStatusTypeValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("İade talebi bulunamadı.")
                .CustomAsync(ValidateDoesReturnRequestExistsAsync);

            RuleFor(e => e.DisapprovedReasonMessage)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("İade talebi reddedildiği için reddedilme sebebi gereklidir.")
                .When(e => e.NewReturnRequestStatusType == ReturnRequestStatusType.DISAPPROVED);
        }

        #region Private Methods

        #region ValidateDoesReturnRequestExistsAsync

        private async Task ValidateDoesReturnRequestExistsAsync(
            long value,
            ValidationContext<UpdateReturnRequest_ReturnRequestStatusType> context,
            CancellationToken cancellationToken)
        {
            var returnRequestExists = await _mediator.Send(new DoesEntityExists<ReturnRequestResponse>(value));

            if (!returnRequestExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateReturnRequest_ReturnRequestStatusType.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "İade talebi bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
