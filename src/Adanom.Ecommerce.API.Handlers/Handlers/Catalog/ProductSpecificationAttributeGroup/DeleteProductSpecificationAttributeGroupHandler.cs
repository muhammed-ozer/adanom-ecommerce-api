using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSpecificationAttributeGroupHandler : IRequestHandler<DeleteProductSpecificationAttributeGroup, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductSpecificationAttributeGroupHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProductSpecificationAttributeGroup command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var productSpecificationAttributeGroup = await applicationDbContext.ProductSpecificationAttributeGroups
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSpecificationAttributeGroup.DeletedAtUtc = DateTime.UtcNow;
            productSpecificationAttributeGroup.DeletedByUserId = userId;

            await applicationDbContext.SaveChangesAsync();

            await _mediator.Publish(new RemoveFromCache<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroup.Id));

            return true;
        }

        #endregion
    }
}
