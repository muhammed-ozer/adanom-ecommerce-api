namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CancelReturnRequestValidator : AbstractValidator<CancelReturnRequest>
    {
        private readonly IMediator _mediator;

        public CancelReturnRequestValidator(IMediator mediator)
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

            RuleFor(e => e)
                .CustomAsync(ValidateDoesReturnRequestCancelableAsync);
        }

        #region Private Methods

        #region ValidateDoesReturnRequestExistsAsync

        private async Task ValidateDoesReturnRequestExistsAsync(
            long value,
            ValidationContext<CancelReturnRequest> context,
            CancellationToken cancellationToken)
        {
            var returnRequestExists = await _mediator.Send(new DoesEntityExists<ReturnRequestResponse>(value));

            if (!returnRequestExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CancelReturnRequest.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "İade talebi bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesReturnRequestCancelableAsync

        private async Task ValidateDoesReturnRequestCancelableAsync(
            CancelReturnRequest value,
            ValidationContext<CancelReturnRequest> context,
            CancellationToken cancellationToken)
        {
            var returnRequest = await _mediator.Send(new GetReturnRequest(value.Id));

            if (returnRequest == null)
            {
                return;
            }

            var returnRequestStatusType = returnRequest.ReturnRequestStatusType.Key;

            if (returnRequestStatusType == ReturnRequestStatusType.CANCEL)
            {
                context.AddFailure(new ValidationFailure(nameof(CancelReturnRequest.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "İade talebi zaten iptal edilmiş."
                });

                return;
            }

            if (returnRequestStatusType != ReturnRequestStatusType.RECEIVED)
            {
                context.AddFailure(new ValidationFailure(nameof(CancelOrder.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "İade talebi işlemde/tamamlanmış olduğu için iptal edilemez."
                });
            }
        }

        #endregion

        #endregion
    }
}
