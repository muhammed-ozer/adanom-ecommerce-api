namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetSliderItemsHandler : IRequestHandler<GetSliderItems, PaginatedData<SliderItemResponse>>

    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetSliderItemsHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<SliderItemResponse>> Handle(GetSliderItems command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var sliderItemsQuery = applicationDbContext.SliderItems
                .AsNoTracking();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter.SliderItemType != null)
                {
                    sliderItemsQuery = sliderItemsQuery.Where(e => e.SliderItemType == command.Filter.SliderItemType);
                }

                #endregion
            }

            var totalCount = sliderItemsQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                sliderItemsQuery = sliderItemsQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var sliderItemesponses = _mapper.Map<IEnumerable<SliderItemResponse>>(await sliderItemsQuery.ToListAsync());

            return new PaginatedData<SliderItemResponse>(
                sliderItemesponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
