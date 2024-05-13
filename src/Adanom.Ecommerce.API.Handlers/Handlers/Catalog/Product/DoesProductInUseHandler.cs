namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductInUseHandler : IRequestHandler<DoesEntityInUse<ProductResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductInUseHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<ProductResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.OrderItems
                .Where(e => e.ProductId == command.Id)
                .AnyAsync();
        }

        #endregion
    }
}
