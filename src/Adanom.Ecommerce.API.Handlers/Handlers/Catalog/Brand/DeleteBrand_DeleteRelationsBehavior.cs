using Adanom.Ecommerce.API.Services.Azure;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteBrand_DeleteRelationsBehavior : IPipelineBehavior<DeleteBrand, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IBlobStorageService _blobStorageService;

        #endregion

        #region Ctor

        public DeleteBrand_DeleteRelationsBehavior(
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

        #region IPipelineBehavior Members

        public async Task<bool> Handle(DeleteBrand command, RequestHandlerDelegate<bool> next, CancellationToken cancellationToken)
        {
            var deleteBrandResponse = await next();

            if (!deleteBrandResponse)
            {
                return deleteBrandResponse;
            }

            var brand = await _applicationDbContext.Brands
                    .SingleAsync(e => e.Id == command.Id && e.DeletedAtUtc.HasValue);

            if (brand.LogoPath.IsNotNullOrEmpty())
            {
                // TODO: Test this handler when azure blob storage created
                await _blobStorageService.DeleteFileAsync(brand.LogoPath);
            }

            #region MetaInformation

            var deleteMetaInformationRequest = new DeleteMetaInformationRequest()
            {
                EntityId = command.Id,
                EntityType = EntityType.BRAND
            };

            var deleteMetaInformationCommand = _mapper
                .Map(deleteMetaInformationRequest, new DeleteMetaInformation(command.Identity));

            await _mediator.Send(deleteMetaInformationCommand);

            #endregion

            #region Images

            var deleteImagesRequest = new DeleteImagesRequest()
            {
                EntityId = command.Id,
                EntityType = EntityType.BRAND
            };

            var deleteImagesCommand = _mapper
                .Map(deleteImagesRequest, new DeleteImages(command.Identity));

            await _mediator.Send(deleteImagesCommand);

            #endregion

            return deleteBrandResponse;
        }

        #endregion
    }
}
