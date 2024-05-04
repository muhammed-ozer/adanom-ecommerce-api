using Adanom.Ecommerce.API.Services.Azure;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteBrand_DeleteRelationsBehavior : IPipelineBehavior<DeleteBrand, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IBlobStorageService _blobStorageService;

        #endregion

        #region Ctor

        public DeleteBrand_DeleteRelationsBehavior(
            ApplicationDbContext applicationDbContext,
            IBlobStorageService blobStorageService)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _blobStorageService = blobStorageService ?? throw new ArgumentNullException(nameof(blobStorageService));
        }

        #endregion

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteBrand command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteBrandResponse = await next();

            if (deleteBrandResponse)
            {
                var brand = await _applicationDbContext.Brands
                    .SingleOrDefaultAsync(e => e.Id == command.Id && e.DeletedAtUtc.HasValue);

                if (brand != null)
                {
                    if (brand.LogoPath.IsNotNullOrEmpty())
                    {
                        // TODO: Test this handler when azure blob storage created
                        await _blobStorageService.DeleteFileAsync(AzureBlobStorageConstants.Containers.Brands, brand.LogoPath);
                    }
                }
            }

            return deleteBrandResponse;
        }

        #endregion
    }
}
