namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductPriceHandler : IRequestHandler<GetProductPrice, ProductPriceResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductPriceHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductPriceResponse?> Handle(GetProductPrice command, CancellationToken cancellationToken)
        {
            var productPrice = await _applicationDbContext.ProductSKUs
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null && e.Id == command.ProductSKUId)
                .Select(e => e.ProductPrice)
                .Where(e => e.DeletedAtUtc == null)
                .SingleOrDefaultAsync();
           
            return _mapper.Map<ProductPriceResponse>(productPrice);
        } 

        #endregion
    }
}
