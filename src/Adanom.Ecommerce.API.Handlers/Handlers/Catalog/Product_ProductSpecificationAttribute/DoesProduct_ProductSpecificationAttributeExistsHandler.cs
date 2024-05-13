namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProduct_ProductSpecificationAttributeExistsHandler : IRequestHandler<DoesProduct_ProductSpecificationAttributeExists, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProduct_ProductSpecificationAttributeExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesProduct_ProductSpecificationAttributeExists command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                .AnyAsync(e => 
                    e.ProductId == command.ProductId && 
                    e.ProductSpecificationAttributeId == command.ProductSpecificationAttributeId);
        }

        #endregion
    }
}
