namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductHandler : IRequestHandler<GetProduct, ProductResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductResponse?> Handle(GetProduct command, CancellationToken cancellationToken)
        {
            Product? product = null;

            if (command.UrlSlug.IsNotNullOrEmpty())
            {
                product = await _applicationDbContext.Products
                    .Where(e => e.DeletedAtUtc == null && e.UrlSlug == command.UrlSlug)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                product = await _applicationDbContext.Products
                    .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }

            return _mapper.Map<ProductResponse>(product);
        } 

        #endregion
    }
}
