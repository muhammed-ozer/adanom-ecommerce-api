using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_ProductCategoryHandler : IRequestHandler<DeleteProduct_ProductCategory, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProduct_ProductCategoryHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProduct_ProductCategory command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var product_ProductCategory = await applicationDbContext.Product_ProductCategory_Mappings
                .Where(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductCategoryId == command.ProductCategoryId)
                .SingleAsync();

            applicationDbContext.Remove(product_ProductCategory);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
