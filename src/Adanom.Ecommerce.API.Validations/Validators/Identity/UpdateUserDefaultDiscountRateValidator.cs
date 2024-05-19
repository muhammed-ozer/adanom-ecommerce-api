using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateUserDefaultDiscountRateValidator : AbstractValidator<UpdateUserDefaultDiscountRate>
    {
        private readonly IMediator _mediator;

        public UpdateUserDefaultDiscountRateValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .NotNull()
                    .WithMessage(e => "Kullanıcı bulunamadı.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .CustomAsync(ValidateDoesUserExistsAsync);
            
            RuleFor(e => (int)e.DefaultDiscountRate)
                .GreaterThanOrEqualTo(0)
                    .WithMessage(e => "Varsayılan iskonto oranı 0 veya daha büyük olmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED)
                .LessThanOrEqualTo(100)
                    .WithMessage(e => "Varsayılan iskonto oranı 100 veya daha küçük olmalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesUserExistsAsync

        private async Task ValidateDoesUserExistsAsync(
            Guid value,
            ValidationContext<UpdateUserDefaultDiscountRate> context,
            CancellationToken cancellationToken)
        {
            var userExists = await _mediator.Send(new DoesUserExists(value));

            if (!userExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateUserDefaultDiscountRate.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Kullanıcı bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
