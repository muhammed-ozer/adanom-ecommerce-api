using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_ProductSpecificationAttributeHandler : IRequestHandler<DeleteProduct_ProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProduct_ProductSpecificationAttributeHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProduct_ProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var product_ProductSpecificationAttribute = await applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                .Where(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductSpecificationAttributeId == command.ProductSpecificationAttributeId)
                .SingleAsync();

            applicationDbContext.Remove(product_ProductSpecificationAttribute);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
