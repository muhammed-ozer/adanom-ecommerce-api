using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSpecificationAttributeHandler : IRequestHandler<DeleteProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductSpecificationAttributeHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productSpecificationAttribute = await _applicationDbContext.ProductSpecificationAttributes
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSpecificationAttribute.DeletedAtUtc = DateTime.UtcNow;
            productSpecificationAttribute.DeletedByUserId = userId;

            await _applicationDbContext.SaveChangesAsync();

            await _mediator.Publish(new RemoveFromCache<ProductSpecificationAttributeResponse>(productSpecificationAttribute.Id));

            return true;
        }

        #endregion
    }
}
