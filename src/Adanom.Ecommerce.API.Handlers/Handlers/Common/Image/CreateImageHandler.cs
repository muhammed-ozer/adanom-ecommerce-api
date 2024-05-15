using System.Security.Claims;
using Adanom.Ecommerce.API.Services.Azure;
using Microsoft.IdentityModel.Tokens;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class CreateImageHandler : IRequestHandler<CreateImage, ImageResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IBlobStorageService _blobStorageService;

        #endregion

        #region Ctor

        public CreateImageHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IMediator mediator,
            IBlobStorageService blobStorageService)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _blobStorageService = blobStorageService ?? throw new ArgumentNullException(nameof(blobStorageService));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<ImageResponse?> Handle(CreateImage command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var (containerName, entityFolderName) = await GetContainerNameAndEntityFolderNameAsync(command.EntityType, command.EntityId);

            if (containerName.IsNullOrEmpty() || entityFolderName.IsNullOrEmpty())
            {
                return null;
            }

            var fileName = $"{Guid.NewGuid()}{command.File.Extension}";

            command.File.Name = $"{entityFolderName}/{AzureBlobStorageConstants.ImagesFolderName}/{fileName}";

            // TODO: Test here after Azure storage created
            var uploadFileResponse = await _blobStorageService.UploadFileAsync(command.File, containerName);

            if (!uploadFileResponse)
            {
                return null;
            }

            if (!command.IsDefault)
            {
                var hasAnyOtherImage = await _applicationDbContext.Images
                    .AsNoTracking()
                    .AnyAsync(e => e.DeletedAtUtc == null &&
                                   e.EntityId == command.EntityId &&
                                   e.EntityType == command.EntityType);

                if (!hasAnyOtherImage)
                {
                    command.IsDefault = true;
                }
            }
            else
            {
                var cuurentDefaultImage = await _applicationDbContext.Images
                    .Where(e => e.DeletedAtUtc == null &&
                                e.EntityType == command.EntityType &&
                                e.EntityId == command.EntityId &&
                                e.IsDefault)
                    .SingleOrDefaultAsync();

                if (cuurentDefaultImage != null)
                {
                    cuurentDefaultImage.IsDefault = false;
                }
            }

            var image = new Image()
            {
                Name = fileName,
                Path = $"{containerName}/{entityFolderName}/{fileName}",
                DisplayOrder = command.DisplayOrder,
                EntityId = command.EntityId,
                EntityType = command.EntityType,
                IsDefault = command.IsDefault,
                CreatedByUserId = userId,
                CreatedAtUtc = DateTime.UtcNow,
            };

            await _applicationDbContext.AddAsync(image);

            try
            {
                await _applicationDbContext.SaveChangesAsync();

                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.IMAGE,
                    TransactionType = TransactionType.CREATE,
                    Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, image.Id),
                }));
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

            var imageResponse = _mapper.Map<ImageResponse>(image);

            return imageResponse;
        }

        #endregion

        #region GetContainerNameAndEntityFolderAsync

        private async Task<(string containerName, string entityFolderName)> GetContainerNameAndEntityFolderNameAsync(EntityType entityType, long entityId)
        {
            switch (entityType)
            {
                case EntityType.PRODUCT:
                    var productResponse = await _mediator.Send(new GetProduct(entityId));

                    return (AzureBlobStorageConstants.Containers.Products, productResponse!.UrlSlug);

                case EntityType.PRODUCTCATEGORY:
                    var productCategoryResponse = await _mediator.Send(new GetProduct(entityId));

                    return (AzureBlobStorageConstants.Containers.ProductCategories, productCategoryResponse!.UrlSlug);

                case EntityType.BRAND:
                    var brand = await _mediator.Send(new GetProductCategory(entityId));

                    return (AzureBlobStorageConstants.Containers.Brands, brand!.UrlSlug);

                default:
                    return (string.Empty, string.Empty);
            }
        }

        #endregion
    }
}
