namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderBillingAddressHandler : IRequestHandler<GetOrderBillingAddress, OrderBillingAddressResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderBillingAddressHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderBillingAddressResponse?> Handle(GetOrderBillingAddress command, CancellationToken cancellationToken)
        {
            var orderBillingAddress = await _applicationDbContext.OrderBillingAddresses
                .Where(e => e.Id == command.Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return _mapper.Map<OrderBillingAddressResponse>(orderBillingAddress);
        } 

        #endregion
    }
}
