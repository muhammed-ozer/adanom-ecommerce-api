using System.Security.Claims;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class DeleteFavoriteItemValidator : AbstractValidator<DeleteFavoriteItem>
    {
        private readonly IMediator _mediator;

        public DeleteFavoriteItemValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e)
               .CustomAsync(ValidateDoesFavoriteItemExistsAsync);
        }

        #region Private Methods

        #region ValidateDoesFavoriteItemExistsAsync

        private async Task ValidateDoesFavoriteItemExistsAsync(
            DeleteFavoriteItem value,
            ValidationContext<DeleteFavoriteItem> context,
            CancellationToken cancellationToken)
        {
            var userId = value.Identity.GetUserId();

            var favoriteItemExists = await _mediator.Send(new DoesUserEntityExists<FavoriteItemResponse>(userId, value.Id));

            if (!favoriteItemExists)
            {
                context.AddFailure(new ValidationFailure(nameof(DeleteFavoriteItem.Id), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_ALLOWED.ToString(),
                    ErrorMessage = "Favori ürün bulunamadı."
                });
            }
        }

        #endregion

        #endregion
    }
}
