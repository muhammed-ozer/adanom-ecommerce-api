namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetTaxAdministrationsHandler : IRequestHandler<GetTaxAdministrations, PaginatedData<TaxAdministrationResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetTaxAdministrationsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<TaxAdministrationResponse>> Handle(GetTaxAdministrations command, CancellationToken cancellationToken)
        {
            var taxAdministrationsQuery = _applicationDbContext.TaxAdministrations
                .AsNoTracking()
                .Where(e => e.DeletedAtUtc== null);

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter.Query != null)
                {
                    taxAdministrationsQuery = taxAdministrationsQuery.Where(e => e.Code.Contains(command.Filter.Query) ||
                                                                                 e.Name.Contains(command.Filter.Query));
                }

                #endregion
            }

            var totalCount = taxAdministrationsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                taxAdministrationsQuery = taxAdministrationsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            taxAdministrationsQuery = taxAdministrationsQuery.OrderBy(e => e.Code);

            var taxAdministrationResponses = _mapper.Map<IEnumerable<TaxAdministrationResponse>>(await taxAdministrationsQuery.ToListAsync());

            return new PaginatedData<TaxAdministrationResponse>(
                taxAdministrationResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
