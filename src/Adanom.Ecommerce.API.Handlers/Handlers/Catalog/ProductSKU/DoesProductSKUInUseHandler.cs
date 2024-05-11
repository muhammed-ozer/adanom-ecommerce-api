namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSKUInUseHandler : IRequestHandler<DoesEntityInUse<ProductSKUResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductSKUInUseHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<ProductSKUResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.OrderItems
                .Where(e => e.ProductSKUId == command.Id)
                .AnyAsync();
        }

        #endregion
    }
}
