namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductByUrlSlugHandler : IRequestHandler<GetProductByUrlSlug, ProductResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductByUrlSlugHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductResponse?> Handle(GetProductByUrlSlug command, CancellationToken cancellationToken)
        {
            var product = await _applicationDbContext.Products
                .Where(e => e.DeletedAtUtc == null && e.UrlSlug == command.UrlSlug)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return _mapper.Map<ProductResponse>(product);
        } 

        #endregion
    }
}
