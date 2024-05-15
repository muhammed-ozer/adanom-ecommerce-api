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
            var image = await _applicationDbContext.Image_Entity_Mappings
                 .AsNoTracking()
                 .Where(e => e.EntityId == command.EntityId &&
                             e.EntityType == command.EntityType &&
                             e.IsDefault)
                 .Include(e => e.Image)
                 .Where(e => e.Image.DeletedAtUtc == null)
                 .SingleOrDefaultAsync();

            var imageResponse = _mapper.Map<ImageResponse>(image);

            return imageResponse;
        }

        #endregion
    }
}
