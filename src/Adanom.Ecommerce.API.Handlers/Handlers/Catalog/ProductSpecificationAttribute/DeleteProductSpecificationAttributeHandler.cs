using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSpecificationAttributeHandler : IRequestHandler<DeleteProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductSpecificationAttributeHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productSpecificationAttribute = await applicationDbContext.ProductSpecificationAttributes
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSpecificationAttribute.DeletedAtUtc = DateTime.UtcNow;
            productSpecificationAttribute.DeletedByUserId = userId;

            await applicationDbContext.SaveChangesAsync();

            await _mediator.Publish(new RemoveFromCache<ProductSpecificationAttributeResponse>(command.Id));

            return true;
        }

        #endregion
    }
}
