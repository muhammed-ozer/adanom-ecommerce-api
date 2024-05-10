namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class UpdateProductNameValidator : AbstractValidator<UpdateProductName>
    {
        private readonly IMediator _mediator;

        public UpdateProductNameValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.Id)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Ürün bulunamadı.")
                .CustomAsync(ValidateDoesProductExistsAsync);

            RuleFor(e => e.Name)
                .NotEmpty()
                    .WithMessage("Ürün adı gereklidir.")
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                .MaximumLength(250)
                    .WithMessage("Ürün adı 250 karakterden fazla olmamalıdır.")
                    .WithErrorCode(ValidationErrorCodesEnum.GREATER_THAN);

            RuleFor(e => e)
                .MustAsync(ValidateDoesProductNameNotExistsAsync)
                    .WithMessage("Bu isimde başka bir ürün bulunmaktadır.")
                    .WithErrorCode(ValidationErrorCodesEnum.NOT_ALLOWED);
        }

        #region Private Methods

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<UpdateProductName> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(UpdateProductName.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region ValidateDoesProductNameNotExistsAsync

        private async Task<bool> ValidateDoesProductNameNotExistsAsync(
            UpdateProductName value,
            CancellationToken cancellationToken)
        {
            var productNameExists = await _mediator.Send(new DoesEntityNameExists<ProductResponse>(value.Name, value.Id));

            if (productNameExists)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion
    }
}
