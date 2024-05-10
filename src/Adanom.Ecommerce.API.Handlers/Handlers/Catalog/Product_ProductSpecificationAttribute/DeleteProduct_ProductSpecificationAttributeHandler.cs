namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProduct_ProductSpecificationAttributeHandler : IRequestHandler<DeleteProduct_ProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProduct_ProductSpecificationAttributeHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteProduct_ProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var product_ProductSpecificationAttribute = await _applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                .Where(e =>
                    e.ProductId == command.ProductId &&
                    e.ProductSpecificationAttributeId == command.ProductSpecificationAttributeId)
                .SingleAsync();

            _applicationDbContext.Remove(product_ProductSpecificationAttribute);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
