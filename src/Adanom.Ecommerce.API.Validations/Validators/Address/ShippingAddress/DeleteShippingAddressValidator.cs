using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteShippingAddressValidator : AbstractValidator<DeleteShippingAddress>
    {
        private readonly IMediator _mediator;

        public DeleteShippingAddressValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
                .CustomAsync(ValidateDoesShippingAddressExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesShippingAddressExistsAsync

        private async Task ValidateDoesShippingAddressExistsAsync(
            DeleteShippingAddress value,
            ValidationContext<DeleteShippingAddress> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var shippingAddressExists = await _mediator.Send(new DoesUserEntityExists<ShippingAddressResponse>(userId, value.Id));

            if (!shippingAddressExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteShippingAddress.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Teslimat adresi bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
