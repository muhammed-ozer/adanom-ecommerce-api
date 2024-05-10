namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductSKUByIdHandler : IRequestHandler<GetProductSKUById, ProductSKUResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductSKUByIdHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductSKUResponse?> Handle(GetProductSKUById command, CancellationToken cancellationToken)
        {
            var productSKU = await _applicationDbContext.ProductSKUs
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return _mapper.Map<ProductSKUResponse>(productSKU);
        } 

        #endregion
    }
}
