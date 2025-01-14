using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_ProductTagHandler : IRequestHandler<DeleteProduct_ProductTag, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProduct_ProductTagHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProduct_ProductTag command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var product_ProductTag = await applicationDbContext.Product_ProductTag_Mappings
                .Where(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductTagId == command.ProductTagId)
                .SingleOrDefaultAsync();

            if (product_ProductTag == null)
            {
                return true;
            }

            applicationDbContext.Remove(product_ProductTag);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
