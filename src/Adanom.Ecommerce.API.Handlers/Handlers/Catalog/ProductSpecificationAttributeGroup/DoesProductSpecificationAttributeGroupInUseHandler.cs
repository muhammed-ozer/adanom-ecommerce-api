namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSpecificationAttributeGroupInUseHandler : IRequestHandler<DoesEntityInUse<ProductSpecificationAttributeGroupResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductSpecificationAttributeGroupInUseHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityInUse<ProductSpecificationAttributeGroupResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductSpecificationAttributes
                .Where(e => e.DeletedAtUtc == null && e.ProductSpecificationAttributeGroupId == command.Id)
                .AnyAsync();
        }

        #endregion
    }
}
