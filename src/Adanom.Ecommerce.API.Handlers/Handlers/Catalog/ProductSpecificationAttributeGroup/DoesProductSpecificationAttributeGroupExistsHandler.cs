namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSpecificationAttributeGroupExistsHandler : IRequestHandler<DoesEntityExists<ProductSpecificationAttributeGroupResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductSpecificationAttributeGroupExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityExists<ProductSpecificationAttributeGroupResponse> command, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.ProductSpecificationAttributeGroups
                .AnyAsync(e => e.DeletedAtUtc == null && e.Id == command.Id);
        }

        #endregion
    }
}
