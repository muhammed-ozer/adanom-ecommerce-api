namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductSpecificationAttribute_DeleteRelationsBehavior : IPipelineBehavior<DeleteProductSpecificationAttribute, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProductSpecificationAttribute_DeleteRelationsBehavior(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductSpecificationAttribute command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductSpecificationAttributeResponse = await next();

            if (deleteProductSpecificationAttributeResponse)
            {
                var product_ProductSpecificationAttribute_Mappings = await _applicationDbContext.Product_ProductSpecificationAttribute_Mappings
                    .ToListAsync();

                _applicationDbContext.RemoveRange(product_ProductSpecificationAttribute_Mappings);

                try
                {
                    await _applicationDbContext.SaveChangesAsync();
                }
                catch (Exception exception)
                {
                    // TODO: Log exception to database
                    Log.Warning($"ProductSpecificationAttribute_Mappings_Delete_Failed: {exception.Message}");

                    return false;
                }
            }

            return deleteProductSpecificationAttributeResponse;
        }

        #endregion
    }
}
