namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetProductAttributeHandler : IRequestHandler<GetProductAttribute, ProductAttributeResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetProductAttributeHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ProductAttributeResponse?> Handle(GetProductAttribute command, CancellationToken cancellationToken)
        {
            var productAttribute = await _applicationDbContext.ProductAttributes
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleOrDefaultAsync();
           
            return _mapper.Map<ProductAttributeResponse>(productAttribute);
        } 

        #endregion
    }
}
