using System.Security.Claims;
using Adanom.Ecommerce.API.Services.Azure;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteImageHandler : IRequestHandler<DeleteImage, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IBlobStorageService _blobStorageService;

        #endregion

        #region Ctor

        public DeleteImageHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper, IMediator mediator, IBlobStorageService blobStorageService)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _blobStorageService = blobStorageService ?? throw new ArgumentNullException(nameof(blobStorageService));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteImage command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var image = await applicationDbContext.Images
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            if (image.IsDefault)
            {
                var randomEntityImage = await applicationDbContext.Images
                    .Where(e => e.EntityType == image.EntityType &&
                                e.EntityId == image.EntityId)
                    .OrderBy(e => e.DisplayOrder)
                    .FirstOrDefaultAsync();

                if (randomEntityImage != null)
                {
                    randomEntityImage.IsDefault = true;
                }

                image.IsDefault = false;
            }

            var deleteImageResponse = await _blobStorageService.DeleteFileAsync(image.Path);

            applicationDbContext.Remove(image);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
