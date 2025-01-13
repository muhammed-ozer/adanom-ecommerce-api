namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetBillingAddressHandler : IRequestHandler<GetBillingAddress, BillingAddressResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetBillingAddressHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<BillingAddressResponse?> Handle(GetBillingAddress command, CancellationToken cancellationToken)
        {
            BillingAddress? billingAddress = null;

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (command.IncludeDeleted != null && command.IncludeDeleted.Value)
            {
                billingAddress = await applicationDbContext.BillingAddresses
                    .Where(e => e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            else
            {
                billingAddress = await applicationDbContext.BillingAddresses
                    .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }

            return _mapper.Map<BillingAddressResponse>(billingAddress);
        }

        #endregion
    }
}
