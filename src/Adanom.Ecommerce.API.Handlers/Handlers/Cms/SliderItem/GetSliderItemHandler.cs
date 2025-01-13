namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetSliderItemHandler : IRequestHandler<GetSliderItem, SliderItemResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetSliderItemHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<SliderItemResponse?> Handle(GetSliderItem command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var sliderItem = await applicationDbContext.SliderItems
                .AsNoTracking()
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();

            return _mapper.Map<SliderItemResponse>(sliderItem);
        }

        #endregion
    }
}
