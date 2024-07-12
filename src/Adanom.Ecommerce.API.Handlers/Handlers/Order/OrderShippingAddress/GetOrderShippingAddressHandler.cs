namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetOrderShippingAddressHandler : IRequestHandler<GetOrderShippingAddress, OrderShippingAddressResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetOrderShippingAddressHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<OrderShippingAddressResponse?> Handle(GetOrderShippingAddress command, CancellationToken cancellationToken)
        {
            var orderShippingAddress = await _applicationDbContext.OrderShippingAddresses
                .Where(e => e.Id == command.Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return _mapper.Map<OrderShippingAddressResponse>(orderShippingAddress);
        } 

        #endregion
    }
}
