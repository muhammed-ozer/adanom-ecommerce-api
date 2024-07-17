namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductPriceByProductIdHandler : IRequestHandler<GetProductPriceByProductId, ProductPriceResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductPriceByProductIdHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductPriceResponse?> Handle(GetProductPriceByProductId command, CancellationToken cancellationToken)
        {
            var productPrice = await _applicationDbContext.Products
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .Select(e => e.ProductSKU.ProductPrice)
                .SingleOrDefaultAsync();
           
            return _mapper.Map<ProductPriceResponse>(productPrice);
        } 

        #endregion
    }
}
