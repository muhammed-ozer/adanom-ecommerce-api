namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DoesProductSpecificationAttributeGroupNameExistsHandler : 
        IRequestHandler<DoesEntityNameExists<ProductSpecificationAttributeGroupResponse>, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;

        #endregion

        #region Ctor

        public DoesProductSpecificationAttributeGroupNameExistsHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DoesEntityNameExists<ProductSpecificationAttributeGroupResponse> command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var query = applicationDbContext.ProductSpecificationAttributeGroups
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
