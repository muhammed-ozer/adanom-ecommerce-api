namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetSliderItemHandler : IRequestHandler<GetSliderItem, SliderItemResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetSliderItemHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<SliderItemResponse?> Handle(GetSliderItem command, CancellationToken cancellationToken)
        {
            var sliderItem = await _applicationDbContext.SliderItems
                .AsNoTracking()
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();
           
            return _mapper.Map<SliderItemResponse>(sliderItem);
        } 

        #endregion
    }
}
