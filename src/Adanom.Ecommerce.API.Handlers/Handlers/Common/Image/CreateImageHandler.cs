using System.Security.Claims;
using Adanom.Ecommerce.API.Services.Azure;
using Microsoft.IdentityModel.Tokens;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateImageHandler : IRequestHandler<CreateImage, ImageResponse?>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IBlobStorageService _blobStorageService;

        #endregion

        #region Ctor

        public CreateImageHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator,
            IBlobStorageService blobStorageService)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _blobStorageService = blobStorageService ?? throw new ArgumentNullException(nameof(blobStorageService));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ImageResponse?> Handle(CreateImage command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var entityFolderName = AzureBlobStorageHelpers.GetFolderName(command.EntityType);

            if (entityFolderName.IsNullOrEmpty())
            {
                return null;
            }

            string fileName;

            if (command.ImageType == ImageType.LOGO)
            {
                fileName = $"logo{command.File.Extension}";
            }
            else
            {
                fileName = $"{Guid.NewGuid()}{command.File.Extension}";
            }

            command.File.Name = $"{entityFolderName}/{command.EntityNameAsUrlSlug}/{AzureBlobStorageConstants.ImagesFolderName}/{fileName}";

            var uploadFileResponse = await _blobStorageService.UploadFileAsync(command.File);

            if (!uploadFileResponse)
            {
                return null;
            }

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            if (!command.IsDefault)
            {
                var hasAnyOtherImage = await applicationDbContext.Images
                    .AsNoTracking()
                    .AnyAsync(e => e.EntityId == command.EntityId &&
                                   e.EntityType == command.EntityType);

                if (!hasAnyOtherImage)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var cuurentDefaultImage = await applicationDbContext.Images
                    .Where(e => e.EntityType == command.EntityType &&
                                e.EntityId == command.EntityId &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (cuurentDefaultImage != null)
                {
                    cuurentDefaultImage.IsDefault = false;
                }
            }

            var image = _mapper.Map<CreateImage, Image>(command, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.Name = fileName;
                    target.CreatedByUserId = userId;
                    target.CreatedAtUtc = DateTime.UtcNow;
                });
            });

            await applicationDbContext.AddAsync(image);

            try
            {
                await applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.IMAGE,
                    TransactionType = TransactionType.CREATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.IMAGE,
                TransactionType = TransactionType.CREATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, image.Id),
            }));

            var imageResponse = _mapper.Map<ImageResponse>(image);

            return imageResponse;
        }

        #endregion
    }
}
