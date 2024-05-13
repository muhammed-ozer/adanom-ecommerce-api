using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSpecificationAttributeGroupHandler : IRequestHandler<DeleteProductSpecificationAttributeGroup, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProductSpecificationAttributeGroupHandler(
            ApplicationDbContext applicationDbContext,
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProductSpecificationAttributeGroup command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var productSpecificationAttributeGroup = await _applicationDbContext.ProductSpecificationAttributeGroups
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            productSpecificationAttributeGroup.DeletedAtUtc = DateTime.UtcNow;
            productSpecificationAttributeGroup.DeletedByUserId = userId;

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"ProductSpecificationAttributeGroup_Delete_Failed: {exception.Message}");

                return false;
            }

            await _mediator.Publish(new RemoveFromCache<ProductSpecificationAttributeGroupResponse>(productSpecificationAttributeGroup.Id));

            return true;
        }

        #endregion
    }
}
