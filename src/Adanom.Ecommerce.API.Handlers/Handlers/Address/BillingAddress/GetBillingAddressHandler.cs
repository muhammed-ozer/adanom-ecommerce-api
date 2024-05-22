namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBillingAddressHandler : IRequestHandler<GetBillingAddress, BillingAddressResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetBillingAddressHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<BillingAddressResponse?> Handle(GetBillingAddress command, CancellationToken cancellationToken)
        {
            BillingAddress? billingAddress = null;

            if (command.IncludeDeleted != null && command.IncludeDeleted.Value)
            {
                billingAddress = await _applicationDbContext.BillingAddresses
                    .Where(e => e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                billingAddress = await _applicationDbContext.BillingAddresses
                    .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }

            return _mapper.Map<BillingAddressResponse>(billingAddress);
        } 

        #endregion
    }
}
