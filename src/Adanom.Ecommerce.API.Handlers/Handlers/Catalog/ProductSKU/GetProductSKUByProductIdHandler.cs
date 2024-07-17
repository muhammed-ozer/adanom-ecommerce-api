namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSKUByProductIdHandler : IRequestHandler<GetProductSKUByProductId, ProductSKUResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductSKUByProductIdHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductSKUResponse?> Handle(GetProductSKUByProductId command, CancellationToken cancellationToken)
        {
            var productSKU = await _applicationDbContext.Products
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .Select(e => e.ProductSKU)
                .SingleOrDefaultAsync();
           
            return _mapper.Map<ProductSKUResponse>(productSKU);
        } 

        #endregion
    }
}
