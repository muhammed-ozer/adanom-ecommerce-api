using System.Security.Claims;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteBillingAddressValidator : AbstractValidator<DeleteBillingAddress>
    {
        private readonly IMediator _mediator;

        public DeleteBillingAddressValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .CustomAsync(ValidateDoesBillingAddressExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesBillingAddressExistsAsync

        private async Task ValidateDoesBillingAddressExistsAsync(
            DeleteBillingAddress value,
            ValidationContext<DeleteBillingAddress> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var billingAddressExists = await _mediator.Send(new DoesUserEntityExists<BillingAddressResponse>(userId, value.Id));

            if (!billingAddressExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteBillingAddress.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Fatura adresi bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
