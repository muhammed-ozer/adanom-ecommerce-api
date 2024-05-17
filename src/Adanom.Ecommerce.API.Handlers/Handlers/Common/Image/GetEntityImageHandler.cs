namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetEntityImageHandler : IRequestHandler<GetEntityImage, ImageResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetEntityImageHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ImageResponse?> Handle(GetEntityImage command, CancellationToken cancellationToken)
        {
            var imagesQuery = _applicationDbContext.Images
                 .AsNoTracking()
                 .Where(e => e.DeletedAtUtc == null &&
                             e.EntityId == command.EntityId &&
                             e.EntityType == command.EntityType);

            Image? image = null;

            if (command.IsDefault != null && command.IsDefault.Value)
            {
                image = await imagesQuery
                    .Where(e => e.IsDefault)
                    .FirstOrDefaultAsync();
            }
            
            if (command.ImageType != null)
            {
                image = await imagesQuery
                    .Where(e => e.ImageType == command.ImageType)
                    .FirstOrDefaultAsync();
            }

            var imageResponse = _mapper.Map<ImageResponse>(image);

            return imageResponse;
        }

        #endregion
    }
}
