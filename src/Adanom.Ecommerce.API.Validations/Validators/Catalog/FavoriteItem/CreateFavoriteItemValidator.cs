using System.Security.Claims;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateFavoriteItemValidator : AbstractValidator<CreateFavoriteItem>
    {
        private readonly IMediator _mediator;

        public CreateFavoriteItemValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.ProductId)
               .GreaterThan(0)
                   .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                   .WithMessage("Ürün bulunamadı.")
               .CustomAsync(ValidateDoesProductExistsAsync);

            RuleFor(e => e)
               .CustomAsync(CanUserAddProductToFavoriteItemsAsync);
        }

        #region Private Methods

        #region ValidateDoesProductExistsAsync

        private async Task ValidateDoesProductExistsAsync(
            long value,
            ValidationContext<CreateFavoriteItem> context,
            CancellationToken cancellationToken)
        {
            var productExists = await _mediator.Send(new DoesEntityExists<ProductResponse>(value));

            if (!productExists)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateFavoriteItem.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün bulunamadı."
                });
            }
        }

        #endregion

        #region CanUserAddProductToFavoriteItemsAsync

        private async Task CanUserAddProductToFavoriteItemsAsync(
            CreateFavoriteItem value,
            ValidationContext<CreateFavoriteItem> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var canUserAddProductToFavoriteItems = await _mediator.Send(new CanUserAddProductToFavoriteItems(userId, value.ProductId));

            if (!canUserAddProductToFavoriteItems)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateFavoriteItem.ProductId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Ürün favori listenize eklenmiş durumda."
                });
            }
        }

        #endregion

        #endregion
    }
}
