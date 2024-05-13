namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateProduct_ProductSpecificationAttributeHandler : IRequestHandler<CreateProduct_ProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public CreateProduct_ProductSpecificationAttributeHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(CreateProduct_ProductSpecificationAttribute command, CancellationToken cancellationToken)
        {
            var product_ProductSpecificationAttribute = new Product_ProductSpecificationAttribute_Mapping()
            {
                ProductId = command.ProductId,
                ProductSpecificationAttributeId = command.ProductSpecificationAttributeId
            };

            await _applicationDbContext.AddAsync(product_ProductSpecificationAttribute);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                // TODO: Log exception to database
                Log.Warning($"Product_ProductSpecificationAttribute_Create_Failed: {exception.Message}");

                return false;
            }

            return true;
        }

        #endregion
    }
}
