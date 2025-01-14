namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetEntityImagesHandler : IRequestHandler<GetEntityImages, IEnumerable<ImageResponse>>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetEntityImagesHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ImageResponse>> Handle(GetEntityImages command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var images = await applicationDbContext.Images
                 .AsNoTracking()
                 .Where(e => e.EntityId == command.EntityId &&
                             e.EntityType == command.EntityType)
                 .OrderBy(e => e.IsDefault)
                 .ThenBy(e => e.DisplayOrder)
                 .ToListAsync();

            var imageResponses = _mapper.Map<IEnumerable<ImageResponse>>(images);

            return imageResponses;
        }

        #endregion
    }
}
