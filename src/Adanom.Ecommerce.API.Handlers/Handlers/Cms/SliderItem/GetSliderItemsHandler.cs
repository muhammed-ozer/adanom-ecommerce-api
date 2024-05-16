namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetSliderItemsHandler : IRequestHandler<GetSliderItems, PaginatedData<SliderItemResponse>>

    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetSliderItemsHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<SliderItemResponse>> Handle(GetSliderItems command, CancellationToken cancellationToken)
        {
            var sliderItemsQuery = _applicationDbContext.SliderItems
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
