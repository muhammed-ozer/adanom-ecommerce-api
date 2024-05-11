namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProduct_ProductTagExistsHandler : IRequestHandler<DoesProduct_ProductTagExists, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProduct_ProductTagExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProduct_ProductTagExists command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Product_ProductTag_Mappings
                .AnyAsync(e => 
                    e.ProductId == command.ProductId && 
                    e.ProductTag.Value == command.ProductTag_Value);
        }

        #endregion
    }
}
