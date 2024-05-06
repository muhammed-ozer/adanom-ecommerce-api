namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteProductTag_DeleteRelationsBehavior : IPipelineBehavior<DeleteProductTag, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DeleteProductTag_DeleteRelationsBehavior(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteProductTag command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteProductTagResponse = await next();

            if (deleteProductTagResponse)
            {
                var product_ProductTag_Mappings = await _applicationDbContext.Product_ProductTag_Mappings
                                .Where(e => e.ProductTagId == command.Id)
                                .ToListAsync();

                if (product_ProductTag_Mappings.Any())
                {
                    _applicationDbContext.RemoveRange(product_ProductTag_Mappings);

                    await _applicationDbContext.SaveChangesAsync();
                }
            }

            return deleteProductTagResponse;
        }

        #endregion
    }
}
