using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateSliderItemHandler : IRequestHandler<CreateSliderItem, SliderItemResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public CreateSliderItemHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<SliderItemResponse?> Handle(CreateSliderItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var sliderItem = _mapper.Map<CreateSliderItem, SliderItem>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            await applicationDbContext.AddAsync(sliderItem);
            await applicationDbContext.SaveChangesAsync();

            var sliderItemResponse = _mapper.Map<SliderItemResponse>(sliderItem);

            return sliderItemResponse;
        }

        #endregion
    }
}
