namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetTaxAdministrationHandler : IRequestHandler<GetTaxAdministration, TaxAdministrationResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetTaxAdministrationHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<TaxAdministrationResponse?> Handle(GetTaxAdministration command, CancellationToken cancellationToken)
        {
            var taxAdministration = await _applicationDbContext.TaxAdministrations
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc == null && e.Id == command.Id)
                .SingleOrDefaultAsync();
           
            return _mapper.Map<TaxAdministrationResponse>(taxAdministration);
        } 

        #endregion
    }
}
