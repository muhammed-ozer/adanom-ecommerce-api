namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSpecificationAttributeGroupNameExistsHandler : 
        IRequestHandler<DoesEntityNameExists<ProductSpecificationAttributeGroupResponse>, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public DoesProductSpecificationAttributeGroupNameExistsHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityNameExists<ProductSpecificationAttributeGroupResponse> command, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.ProductSpecificationAttributeGroups
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null &&
                            e.Name.ToLower() == command.Name.ToLower());

            if (command.ExcludedEntityId != null)
            {
                query = query.Where(e => e.Id != command.ExcludedEntityId);
            }

            return await query.AnyAsync();
        }

        #endregion
    }
}
