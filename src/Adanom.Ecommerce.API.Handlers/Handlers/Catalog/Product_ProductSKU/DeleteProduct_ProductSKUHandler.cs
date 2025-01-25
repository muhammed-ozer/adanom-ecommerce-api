namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_ProductSKUHandler : IRequestHandler<DeleteProduct_ProductSKU, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteProduct_ProductSKUHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProduct_ProductSKU command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var product_ProductSKU = await applicationDbContext.Product_ProductSKU_Mappings
                .Where(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductSKUId == command.ProductSKUId)
                .SingleAsync();

            applicationDbContext.Remove(product_ProductSKU);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
