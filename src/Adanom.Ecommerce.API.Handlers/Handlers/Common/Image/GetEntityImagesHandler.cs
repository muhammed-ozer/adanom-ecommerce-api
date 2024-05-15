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
            var image_EntitesQuery = _applicationDbContext.Image_Entity_Mappings
                 .AsNoTracking()
                 .Where(e => e.EntityId == command.EntityId &&
                             e.EntityType == command.EntityType)
                 .Include(e => e.Image)
                 .Where(e => e.Image.DeletedAtUtc == null)
                 .OrderBy(e => e.Image.DisplayOrder);

            var imageResponses = _mapper.Map<IEnumerable<ImageResponse>>(await image_EntitesQuery.ToListAsync());

            return imageResponses;
        }

        #endregion
    }
}
