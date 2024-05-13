using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteBrandHandler : IRequestHandler<DeleteBrand, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteBrandHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteBrand command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var brand = await _applicationDbContext.Brands
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            brand.DeletedAtUtc = DateTime.UtcNow;
            brand.DeletedByUserId = userId;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"Brand_Delete_Failed: {exception.Message}");

                return false;
            }

            await _mediator.Publish(new RemoveFromCache<BrandResponse>(brand.Id));

            return true;
        }

        #endregion
    }
}
