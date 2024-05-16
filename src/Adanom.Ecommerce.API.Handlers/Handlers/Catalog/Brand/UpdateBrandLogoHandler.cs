using System.Security.Claims;
using Adanom.Ecommerce.API.Services.Azure;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateBrandLogoHandler : IRequestHandler<UpdateBrandLogo, BrandResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IBlobStorageService _blobStorageService;

        #endregion

        #region Ctor

        public UpdateBrandLogoHandler(
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

        public async Task<BrandResponse?> Handle(UpdateBrandLogo command, CancellationToken cancellationToken)
        {
            // TODO: Test this handler when azure blob storage created

            var userId = command.Identity.GetUserId();

            var brand = await _applicationDbContext.Brands
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            var entityFolderName = AzureBlobStorageHelpers.GetFolderName(EntityType.BRAND);
            var logoPath = $"{entityFolderName}/{brand.UrlSlug}/{AzureBlobStorageConstants.ImagesFolderName}/logo{command.Logo.Extension}";

            command.Logo.Name = logoPath;

            var uploadFileResponse = await _blobStorageService.UploadFileAsync(
                command.Logo,
                brand.LogoPath);

            brand.LogoPath = logoPath;
            brand.UpdatedAtUtc = DateTime.UtcNow;
            brand.UpdatedByUserId = userId;

            _applicationDbContext.Update(brand);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.BRAND,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return null;
            }

            var brandResponse = _mapper.Map<BrandResponse>(brand);

            await _mediator.Publish(new UpdateFromCache<BrandResponse>(brandResponse));

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.BRAND,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, brand.Id),
            }));

            return brandResponse;
        }

        #endregion
    }
}
