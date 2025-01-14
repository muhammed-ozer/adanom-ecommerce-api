using System.Security.Claims;
using Adanom.Ecommerce.API.Services.Azure;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateBrandLogoHandler : IRequestHandler<UpdateBrandLogo, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateBrandLogoHandler(
            IDbContextFactory<ApplicationDbContext> applicationDbContextFactory,
            IMapper mapper,
            IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateBrandLogo command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var brand = await applicationDbContext.Brands
                .Where(e => e.DeletedAtUtc == null &&
                            e.Id == command.Id)
                .SingleAsync();

            var currentLogoImage = await _mediator.Send(new GetEntityImage(brand.Id, EntityType.BRAND, ImageType.LOGO));

            if (currentLogoImage != null)
            {
                var deleteImageRequest = new DeleteImageRequest()
                {
                    Id = currentLogoImage.Id,
                };

                var deleteImageCommand = _mapper.Map(deleteImageRequest, new DeleteImage(command.Identity));

                var deleteImageResult = await _mediator.Send(deleteImageCommand);

                if (!deleteImageResult)
                {
                    return false;
                }
            }

            var createImageRequest = new CreateImageRequest()
            {
                File = command.File,
                EntityId = brand.Id,
                EntityType = EntityType.BRAND,
                ImageType = ImageType.LOGO,
                EntityNameAsUrlSlug = brand.UrlSlug
            };

            var createImageCommand = _mapper
                    .Map(createImageRequest, new CreateImage(command.Identity));

            var createImageResponse = await _mediator.Send(createImageCommand);

            if (createImageResponse == null)
            {
                return false;
            }

            brand.UpdatedAtUtc = DateTime.UtcNow;
            brand.UpdatedByUserId = userId;

            applicationDbContext.Update(brand);
            await applicationDbContext.SaveChangesAsync();

            var brandResponse = _mapper.Map<BrandResponse>(brand);

            brandResponse.Logo = createImageResponse;

            await _mediator.Publish(new UpdateFromCache<BrandResponse>(brandResponse));

            return true;
        }

        #endregion
    }
}
