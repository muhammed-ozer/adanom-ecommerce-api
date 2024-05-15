namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetEntityDefaultImageHandler : IRequestHandler<GetEntityDefaultImage, ImageResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetEntityDefaultImageHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ImageResponse?> Handle(GetEntityDefaultImage command, CancellationToken cancellationToken)
        {
            var image = await _applicationDbContext.Images
                 .AsNoTracking()
                 .Where(e => e.DeletedAtUtc == null &&
                             e.EntityId == command.EntityId &&
                             e.EntityType == command.EntityType &&
                             e.IsDefault)
                 .SingleOrDefaultAsync();

            var imageResponse = _mapper.Map<ImageResponse>(image);

            return imageResponse;
        }

        #endregion
    }
}
