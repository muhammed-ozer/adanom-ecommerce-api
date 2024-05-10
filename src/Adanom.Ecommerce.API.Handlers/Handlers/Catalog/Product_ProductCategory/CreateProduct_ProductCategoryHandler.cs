namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_ProductCategoryHandler : IRequestHandler<CreateProduct_ProductCategory, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public CreateProduct_ProductCategoryHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateProduct_ProductCategory command, CancellationToken cancellationToken)
        {
            var product_ProductCategory = new Product_ProductCategory_Mapping()
            {
                ProductId = command.ProductId,
                ProductCategoryId = command.ProductCategoryId
            };

            await _applicationDbContext.AddAsync(product_ProductCategory);

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
