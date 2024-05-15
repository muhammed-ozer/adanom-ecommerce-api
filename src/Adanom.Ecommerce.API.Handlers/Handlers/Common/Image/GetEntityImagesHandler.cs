namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetEntityImagesHandler : IRequestHandler<GetEntityImages, IEnumerable<ImageResponse>>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetEntityImagesHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<IEnumerable<ImageResponse>> Handle(GetEntityImages command, CancellationToken cancellationToken)
        {
            var images = await _applicationDbContext.Images
                 .AsNoTracking()
                 .Where(e => e.DeletedAtUtc == null &&
                             e.EntityId == command.EntityId &&
                             e.EntityType == command.EntityType)
                 .OrderBy(e => e.DisplayOrder)
                 .ToListAsync();

            var imageResponses = _mapper.Map<IEnumerable<ImageResponse>>(images);

            return imageResponses;
        }

        #endregion
    }
}
