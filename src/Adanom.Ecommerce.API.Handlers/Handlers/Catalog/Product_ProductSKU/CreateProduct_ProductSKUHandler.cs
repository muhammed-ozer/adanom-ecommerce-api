namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_ProductSKUHandler : IRequestHandler<CreateProduct_ProductSKU, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateProduct_ProductSKUHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateProduct_ProductSKU command, CancellationToken cancellationToken)
        {
            var product_ProductSKU = new Product_ProductSKU_Mapping()
            {
                ProductId = command.ProductId,
                ProductSKUId = command.ProductSKUId
            };

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(product_ProductSKU);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
