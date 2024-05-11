namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSKUHandler : IRequestHandler<GetProductSKU, ProductSKUResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductSKUHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductSKUResponse?> Handle(GetProductSKU command, CancellationToken cancellationToken)
        {
            ProductSKU? productSKU = null;

            if (command.Code.IsNotNullOrEmpty())
            {
                productSKU = await _applicationDbContext.ProductSKUs
                    .Where(e => e.DeletedAtUtc == null && e.Code == command.Code)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                productSKU = await _applicationDbContext.ProductSKUs
                    .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            
            return _mapper.Map<ProductSKUResponse>(productSKU);
        } 

        #endregion
    }
}
