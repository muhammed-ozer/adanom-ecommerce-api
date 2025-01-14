namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductHandler : IRequestHandler<GetProduct, ProductResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductResponse?> Handle(GetProduct command, CancellationToken cancellationToken)
        {
            Product? product = null;

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (command.UrlSlug.IsNotNullOrEmpty())
            {
                product = await applicationDbContext.Products
                    .Where(e => e.DeletedAtUtc == null && e.UrlSlug == command.UrlSlug)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                product = await applicationDbContext.Products
                    .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }

            return _mapper.Map<ProductResponse>(product);
        } 

        #endregion
    }
}
